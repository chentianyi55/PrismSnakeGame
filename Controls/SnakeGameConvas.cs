using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PrismSnakeGame.ViewModels;

namespace PrismSnakeGame.Controls
{
    public class SnakeGameConvas : Control
    {
        static SnakeGameConvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SnakeGameConvas), new FrameworkPropertyMetadata(typeof(SnakeGameConvas)));
        }

        //依赖属性：蛇身体坐标
        public static readonly DependencyProperty SnakePositionsProperty =
            DependencyProperty.Register(nameof(SnakePositions), typeof(ObservableCollection<Point>),
                typeof(SnakeGameConvas),new PropertyMetadata(null,OnSnakePositionsChanged));

        public ObservableCollection<Point> SnakePositions
        {
            get => GetValue(SnakePositionsProperty) as ObservableCollection<Point>;
            set => SetValue(SnakePositionsProperty, value);
        }

        // 依赖属性：食物坐标
        public static readonly DependencyProperty FoodPositionProperty =
            DependencyProperty.Register(nameof(FoodPosition), typeof(Point),
                typeof(SnakeGameConvas), new PropertyMetadata(default(Point), OnFoodPositionChanged));

        public Point FoodPosition
        {
            get => (Point)GetValue(FoodPositionProperty);
            set => SetValue(FoodPositionProperty, value);
        }

        // 依赖属性：网格大小
        public static readonly DependencyProperty GridSizeProperty =
            DependencyProperty.Register(nameof(GridSize), typeof(double),
                typeof(SnakeGameConvas), new PropertyMetadata(20.0, OnGridSizeChanged));

        public double GridSize
        {
            get => (double)GetValue(GridSizeProperty);
            set => SetValue(GridSizeProperty, value);
        }

        private Canvas _canvas;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _canvas = GetTemplateChild("PART_Canvas") as Canvas;
            UpdateVisuals();
        }

        private static void OnSnakePositionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) 
        { 
            var control = d as SnakeGameConvas;
            if (e.OldValue is ObservableCollection<Point> oldCollection)
            {
                oldCollection.CollectionChanged -= control.OnSnakePositionsCollectionChanged;
            }
            if (e.NewValue is ObservableCollection<Point> newCollection) 
            {
                newCollection.CollectionChanged += control.OnSnakePositionsCollectionChanged;
            }

            control.UpdateVisuals();
           
        }

        private static void OnFoodPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        { 
             (d as SnakeGameConvas).UpdateVisuals();
        }

        private static void OnGridSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as SnakeGameConvas).UpdateVisuals();
        }
        private void OnSnakePositionsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateVisuals();
        }

        private void UpdateVisuals()
        { 
        
        }
        }
}
