using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Drawing;
using Drawing = System.Drawing;
using Color = System.Windows.Media.Color;

namespace SoftwareTeamwork
{
    class ColorAlphaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Color.Subtract((Color)value, Color.FromArgb(60, 0, 0, 0));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class IntToColorConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Color color = Colors.Transparent;
            switch ((string)parameter) {
                case "A":
                    color = Color.FromArgb(System.Convert.ToByte(value), 255, 255, 255);
                    break;
                case "R":
                    color = Color.FromArgb(255, System.Convert.ToByte(value), 0, 0);
                    break;
                case "G":
                    color = Color.FromArgb(255, 0, System.Convert.ToByte(value), 0);
                    break;
                case "B":
                    color = Color.FromArgb(255, 0, 0, System.Convert.ToByte(value));
                    break;
            }
            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    class DrawingToMediaConvert : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Drawing.Color co = (Drawing.Color)value;
            return new SolidColorBrush(Color.FromArgb(co.A, co.R, co.G, co.B));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    class DubtoGridlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new GridLength((double)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class ColortoBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new SolidColorBrush((Color)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 将字符转化为路径
    /// </summary>
    class StrToPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Geometry.Parse((String)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 将源位移一个double后返回
    /// </summary>
    class MoveConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return (double)value + System.Convert.ToInt32((string)parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    class DateTimeStringFormat : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return String.Format("{0:D2}.{1:D2}", ((DateTime)value).Month, ((DateTime)value).Day);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    class ActrualDataToNodesPos : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return (int)value * (double)parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    class NumToStringConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return ((int)value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return System.Convert.ToInt32(value);
        }
    }

    class DouToStringConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is null)
                return "";
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return System.Convert.ToDouble(value);
        }
    }

    class WindowToIntPtrConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new WindowInteropHelper(value as Window).Handle;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class FluxFormatConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if ((double)value > 1000) {
                if (parameter is null)
                    return String.Format("{0:###.#} G", (double)value / 1000.0);
                else
                    return String.Format("{0:### . ##} G", (double)value / 1000.0);
            }
            else {
                if (parameter is null)
                    return String.Format("{0:###.#} M", (double)value);
                else
                    return String.Format("{0:### . ##} G", (double)value / 1000.0);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    class TitleStyleConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is int)
                switch (System.Convert.ToInt32(value)) {
                    case 0: return "星期三 - 三";
                    case 1: return "星期三 - 3";
                    case 2: return "WED - 三";
                    case 3: return "WED - 3";
                    case 4: return "三 - 三";
                    default: return "三 - 3";
                }
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }

    class TodayConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (System.Convert.ToInt32(DateTime.Now.DayOfWeek).Equals(System.Convert.ToInt32(parameter)))
                return value;
            else
                return Colors.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}