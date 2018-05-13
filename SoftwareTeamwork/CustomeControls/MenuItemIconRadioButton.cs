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

namespace SoftwareTeamwork {
    public class MenuItemIconRadioButton : RadioButton {

        #region Properties
        public string Text {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public Brush IconMaskN {
            get { return (Brush)GetValue(IconMaskNProperty); }
            set { SetValue(IconMaskNProperty, value); }
        }
        public Brush IconMaskR {
            get { return (Brush)GetValue(IconMaskRProperty); }
            set { SetValue(IconMaskRProperty, value); }
        }
        public Brush IconMaskP {
            get { return (Brush)GetValue(IconMaskPProperty); }
            set { SetValue(IconMaskPProperty, value); }
        }
        public string ForeIcon {
            get { return (string)GetValue(ForeIconProperty); }
            set { SetValue(ForeIconProperty, value); }
        }
        public string BackIcon {
            get { return (string)GetValue(BackIconProperty); }
            set { SetValue(BackIconProperty, value); }
        }
        public double ContentTextFontSize {
            get { return (double)GetValue(ContentTextFontSizeProperty); }
            set { SetValue(ContentTextFontSizeProperty, value); }
        }
        #endregion

        #region DependencyPropertys
        public static readonly DependencyProperty IconMaskNProperty =
    DependencyProperty.Register("IconMaskN", typeof(Brush), typeof(MenuItemIconRadioButton),
        new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));
        public static readonly DependencyProperty IconMaskRProperty =
    DependencyProperty.Register("IconMaskR", typeof(Brush), typeof(MenuItemIconRadioButton),
        new PropertyMetadata(new SolidColorBrush(Color.FromArgb(80, 0, 0, 0))));
        public static readonly DependencyProperty IconMaskPProperty =
     DependencyProperty.Register("IconMaskP", typeof(Brush), typeof(MenuItemIconRadioButton),
         new PropertyMetadata(new SolidColorBrush(Color.FromArgb(160, 0, 0, 0))));
        public static readonly DependencyProperty TextProperty = 
            DependencyProperty.Register("Text", typeof(string), typeof(MenuItemIconRadioButton), 
                new PropertyMetadata(""));
        public static readonly DependencyProperty ForeIconProperty = 
            DependencyProperty.Register("ForeIcon", typeof(string), typeof(MenuItemIconRadioButton), 
                new PropertyMetadata(""));
        public static readonly DependencyProperty BackIconProperty = 
            DependencyProperty.Register("BackIcon", typeof(string), typeof(MenuItemIconRadioButton), 
                new PropertyMetadata(""));
        public static readonly DependencyProperty ContentTextFontSizeProperty =
         DependencyProperty.Register("ContentTextFontSize", typeof(double), typeof(MenuItemIconRadioButton), 
             new PropertyMetadata(0.0));
        #endregion


        public MenuItemIconRadioButton() {
            
        }

        static MenuItemIconRadioButton() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuItemIconRadioButton), new FrameworkPropertyMetadata(typeof(MenuItemIconRadioButton)));
        }
    }
}
