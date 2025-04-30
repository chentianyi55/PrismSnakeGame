using System;
using System.Collections.Generic;
using System.Windows;

namespace PrismSnakeGame.Models
{
    public class SnakeGameModel
    {
        public List<Point> Snake { get; private set; }
        public Point Food { get; private set; }
        public int Score { get; private set; }
        public bool IsGameOver { get; private set; }
        public int Width { get; }
        public int Height { get; }

        private Random random;

        public SnakeGameModel(int width, int height)
        {
            Width = width;
            Height = height;
            random = new Random();
        }

        public void Initialize()
        {
            // 蛇初始位置在网格中央，向右移动
            Snake = new List<Point> { new Point(10, 10), new Point(9, 10), new Point(8, 10) };
            GenerateFood();
            Score = 0;
            IsGameOver = false;
        }

        public void MoveSnake(Direction direction)
        {
            if (IsGameOver) return;

            Point newHead = GetNewHeadPosition(direction);
            if (CheckCollision(newHead))
            {
                IsGameOver = true;
                return;
            }

            if (newHead == Food)
            {
                Snake.Insert(0, newHead); // 吃到食物，蛇身延长
                GenerateFood();
                Score++;
            }
            else
            {
                Snake.Insert(0, newHead); // 移动蛇头
                Snake.RemoveAt(Snake.Count - 1); // 移除蛇尾
            }
        }

        private Point GetNewHeadPosition(Direction direction)
        {
            Point head = Snake[0];
            switch (direction)
            {
                case Direction.Up: return new Point(head.X, head.Y - 1);
                case Direction.Down: return new Point(head.X, head.Y + 1);
                case Direction.Left: return new Point(head.X - 1, head.Y);
                case Direction.Right: return new Point(head.X + 1, head.Y);
                default: return head;
            }
        }

        private bool CheckCollision(Point newHead)
        {
            // 检查边界
            if (newHead.X < 0 || newHead.X >= Width || newHead.Y < 0 || newHead.Y >= Height)
                return true;

            // 检查是否撞到自身
            for (int i = 1; i < Snake.Count; i++)
            {
                if (Snake[i] == newHead)
                    return true;
            }
            return false;
        }

        private void GenerateFood()
        {
            Point newFood;
            do
            {
                newFood = new Point(random.Next(0, Width), random.Next(0, Height)); //初始生成食物
            } while (Snake.Contains(newFood));//是否已经吃到食物，吃到了就再次生成
            Food = newFood;
        }
    }
}