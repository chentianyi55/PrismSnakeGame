using System;
using System.Globalization;
using System.Windows.Data;

namespace PrismSnakeGame.ViewModels
{
    public class PointToPositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double coordinate = (double)value;
            return coordinate * 20; // 每个网格20像素
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}