
using PrismSnakeGame.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrismSnakeGame.Views
{
    public partial class SnakeGame : UserControl
    {
        private SnakeGameViewModel viewModel;

        public SnakeGame()
        {
            InitializeComponent();
            this.viewModel = new SnakeGameViewModel();
            DataContext = viewModel;
            this.KeyDown += SnakeGameView_KeyDown;
            this.Focusable = true;
            this.Loaded += (s, e) => { this.Focus(); };
        }

        private async void SnakeGameView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up: await viewModel.SetDirectionAsync(Direction.Up); break;
                case Key.Down: await viewModel.SetDirectionAsync(Direction.Down); break;
                case Key.Left: await viewModel.SetDirectionAsync(Direction.Left); break;
                case Key.Right: await viewModel.SetDirectionAsync(Direction.Right); break;
            }
            e.Handled = true; //标记事件已处理，防止冒泡
        }

        private void SnakeGameView_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focus(); // 确保加载时获得焦点
            Keyboard.Focus(this); // 强制键盘焦点到当前控件
        }
    }
}