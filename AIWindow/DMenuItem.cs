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

namespace AIWindow
{
    public class DMenuItem : MenuItem
    {
        #region Text&Icon Color
        public Color TextColorN
        {
            get { return (Color)GetValue(TextColorNProperty); }
            set { SetValue(TextColorNProperty, value); }
        }
        public static readonly DependencyProperty TextColorNProperty =
            DependencyProperty.Register("TextColorN", typeof(Color), typeof(DMenuItem), new PropertyMetadata(Color.FromArgb(255, 255, 255, 255)));

        public Color IconColorN
        {
            get { return (Color)GetValue(IconColorNProperty); }
            set { SetValue(IconColorNProperty, value); }
        }
        public static readonly DependencyProperty IconColorNProperty =
            DependencyProperty.Register("IconColorN", typeof(Color), typeof(DMenuItem), new PropertyMetadata(Color.FromArgb(255, 255, 255, 255)));

        public Color TextColorR
        {
            get { return (Color)GetValue(TextColorRProperty); }
            set { SetValue(TextColorRProperty, value); }
        }
        public static readonly DependencyProperty TextColorRProperty =
            DependencyProperty.Register("TextColorR", typeof(Color), typeof(DMenuItem), new PropertyMetadata(Color.FromArgb(255, 0, 0, 0)));

        public Color IconColorR
        {
            get { return (Color)GetValue(IconColorRProperty); }
            set { SetValue(IconColorRProperty, value); }
        }
        public static readonly DependencyProperty IconColorRProperty =
            DependencyProperty.Register("IconColorR", typeof(Color), typeof(DMenuItem), new PropertyMetadata(Color.FromArgb(255, 0, 0, 0)));

        public Color TextMask
        {
            get { return (Color)GetValue(TextMaskProperty); }
            set { SetValue(TextMaskProperty, value); }
        }
        public static readonly DependencyProperty TextMaskProperty =
            DependencyProperty.Register("TextMask", typeof(Color), typeof(DMenuItem), new PropertyMetadata(Color.FromArgb(128,0,0,0)));

        public Color IconMask
        {
            get { return (Color)GetValue(IconMaskProperty); }
            set { SetValue(IconMaskProperty, value); }
        }
        public static readonly DependencyProperty IconMaskProperty =
            DependencyProperty.Register("IconMask", typeof(Color), typeof(DMenuItem), new PropertyMetadata(Color.FromArgb(255, 255, 255, 255)));
        #endregion

        static DMenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DMenuItem), new FrameworkPropertyMetadata(typeof(DMenuItem)));
        }
    }
}
