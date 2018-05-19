using System;
using System.Collections.Generic;
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

namespace SoftwareTeamwork
{
    public class DMenuItem : MenuItem
    {
        #region Color
        public Brush IconMaskN {
            get { return (Brush)GetValue(IconMaskNProperty); }
            set { SetValue(IconMaskNProperty, value); }
        }
        public static readonly DependencyProperty IconMaskNProperty =
            DependencyProperty.Register("IconMaskN", typeof(Brush), typeof(DMenuItem),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public Brush IconMaskR {
            get { return (Brush)GetValue(IconMaskRProperty); }
            set { SetValue(IconMaskRProperty, value); }
        }
        public static readonly DependencyProperty IconMaskRProperty =
            DependencyProperty.Register("IconMaskR", typeof(Brush), typeof(DMenuItem),
                new PropertyMetadata(new SolidColorBrush(Color.FromArgb(160, 0, 0, 0))));

        public Brush IconMaskP {
            get { return (Brush)GetValue(IconMaskPProperty); }
            set { SetValue(IconMaskPProperty, value); }
        }
        public static readonly DependencyProperty IconMaskPProperty =
            DependencyProperty.Register("IconMaskP", typeof(Brush), typeof(DMenuItem),
                new PropertyMetadata(new SolidColorBrush(Color.FromArgb(160, 0, 0, 0))));

        public Brush IconN {
            get { return (Brush)GetValue(IconNProperty); }
            set { SetValue(IconNProperty, value); }
        }
        public static readonly DependencyProperty IconNProperty =
            DependencyProperty.Register("IconN", typeof(Brush), typeof(DMenuItem),
                new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public Brush IconR {
            get { return (Brush)GetValue(IconRProperty); }
            set { SetValue(IconRProperty, value); }
        }
        public static readonly DependencyProperty IconRProperty =
            DependencyProperty.Register("IconR", typeof(Brush), typeof(DMenuItem),
                new PropertyMetadata(new SolidColorBrush(Colors.FloralWhite)));

        public Brush IconP {
            get { return (Brush)GetValue(IconPProperty); }
            set { SetValue(IconPProperty, value); }
        }
        public static readonly DependencyProperty IconPProperty =
            DependencyProperty.Register("IconP", typeof(Brush), typeof(DMenuItem),
                new PropertyMetadata(new SolidColorBrush(Colors.FloralWhite)));

        public Brush TextN {
            get { return (Brush)GetValue(TextNProperty); }
            set { SetValue(TextNProperty, value); }
        }
        public static readonly DependencyProperty TextNProperty =
            DependencyProperty.Register("TextN", typeof(Brush), typeof(DMenuItem),
                new PropertyMetadata(new SolidColorBrush(Colors.FloralWhite)));

        public Brush TextR {
            get { return (Brush)GetValue(TextRProperty); }
            set { SetValue(TextRProperty, value); }
        }
        public static readonly DependencyProperty TextRProperty =
            DependencyProperty.Register("TextR", typeof(Brush), typeof(DMenuItem),
                new PropertyMetadata(new SolidColorBrush(Colors.FloralWhite)));

        public Brush TextP {
            get { return (Brush)GetValue(TextPProperty); }
            set { SetValue(TextPProperty, value); }
        }
        public static readonly DependencyProperty TextPProperty =
            DependencyProperty.Register("TextP", typeof(Brush), typeof(DMenuItem),
                new PropertyMetadata(new SolidColorBrush(Colors.FloralWhite)));
        #endregion

        #region FontIcon
        public string FontIcon {
            get { return (string)GetValue(FontIconProperty); }
            set { SetValue(FontIconProperty, value); }
        }
        public static readonly DependencyProperty FontIconProperty =
            DependencyProperty.Register("FontIcon", typeof(string), 
                typeof(DMenuItem), new PropertyMetadata(""));
        #endregion

        #region Content
        public string Content {
            get { return (string)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(string),
                typeof(DMenuItem), new PropertyMetadata(""));
        #endregion


        static DMenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DMenuItem), new FrameworkPropertyMetadata(typeof(DMenuItem)));
        }
    }
}
