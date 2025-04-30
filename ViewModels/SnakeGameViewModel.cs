using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;
using PrismSnakeGame.Models;
using Prism.Commands;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace PrismSnakeGame.ViewModels
{
    public class SnakeGameViewModel : BindableBase
    {
        private readonly SnakeGameModel model;
        private DispatcherTimer timer;
        private Direction currentDirection;
        private readonly Channel<Direction> directionChannel;

        public ObservableCollection<Point> SnakePositions { get; }
        private Point _foodPosition;

        public DelegateCommand StartGameCommand { get; }
        public Point FoodPosition
        {
            get => _foodPosition;
            set => SetProperty(ref _foodPosition, value);
        }

        private int _score;
        public int Score
        {
            get => _score;
            set => SetProperty(ref _score, value);
        }

        private bool _isGameOver;
        public bool IsGameOver
        {
            get => _isGameOver;
            set => SetProperty(ref _isGameOver, value);
        }
        private int _queueCount;
        public int QueueCount
        {
            get => _queueCount;
            set => SetProperty(ref _queueCount, value);
        }
        public SnakeGameViewModel()
        {
            model = new SnakeGameModel(20, 20); // 20x20 网格
            SnakePositions = new ObservableCollection<Point>();
            directionChannel = Channel.CreateUnbounded<Direction>();
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1000) // 每200ms移动一次
            };
            timer.Tick += Timer_Tick;
            StartGameCommand = new DelegateCommand(StartGame);
        }

        public void StartGame()
        {
            model.Initialize();
            currentDirection = Direction.Right;
            UpdateSnakePositions();
            FoodPosition = model.Food;
            Score = 0;
            IsGameOver = false;
            timer.Start();

            Task.Run(ConsumeDirectionAsync);
        }
        private async Task ConsumeDirectionAsync() 
        {
            while (!IsGameOver)
            {
                if(await directionChannel.Reader.WaitToReadAsync())
                {
                    if(directionChannel.Reader.TryRead(out Direction direction))
                    {
                        if (CanChangeDirection(direction))
                        {
                            currentDirection = direction;
                            Console.WriteLine($"Consumed direction: {direction} at {DateTime.Now}");
                            await Task.Delay(1000); // 延迟1秒，模拟慢速消费
                            QueueCount = directionChannel.Reader.Count;
                        }
                    }
                }
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            model.MoveSnake(currentDirection);
            if (model.IsGameOver)
            {
                timer.Stop();
                IsGameOver = true;
            }
            else
            {
                UpdateSnakePositions();
                FoodPosition = model.Food;
                Score = model.Score;
            }
        }

        private void UpdateSnakePositions()
        {
            SnakePositions.Clear();
            foreach (var point in model.Snake)
            {
                SnakePositions.Add(point);
            }
        }

        public async Task SetDirectionAsync(Direction direction)
        {
            //if (CanChangeDirection(direction))
            //{
            //    currentDirection = direction;
            //}
            await directionChannel.Writer.WriteAsync(direction);
            QueueCount = directionChannel.Reader.Count; // 更新队列计数
            Console.WriteLine($"Produced direction: {direction}, Queue count: {QueueCount}");
        }

        private bool CanChangeDirection(Direction newDirection)
        {
            // 防止蛇反向移动
            if (currentDirection == Direction.Up && newDirection == Direction.Down) return false;
            if (currentDirection == Direction.Down && newDirection == Direction.Up) return false;
            if (currentDirection == Direction.Left && newDirection == Direction.Right) return false;
            if (currentDirection == Direction.Right && newDirection == Direction.Left) return false;
            return true;
        }


    }
}