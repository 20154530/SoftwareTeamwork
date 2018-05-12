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

        #region Text
        public string Text {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        #endregion

        #region
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MenuItemIconRadioButton),
        new PropertyMetadata(""));
        #endregion

        static MenuItemIconRadioButton() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuItemIconRadioButton), new FrameworkPropertyMetadata(typeof(MenuItemIconRadioButton)));
        }
    }
}
