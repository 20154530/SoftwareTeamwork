using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoftwareTeamwork {
    public class IconToggelButton : ToggleButton {

        #region ForeIcon
        public string ForeIcon {
            get { return (string)GetValue(ForeIconProperty); }
            set { SetValue(ForeIconProperty, value); }
        }
        public static readonly DependencyProperty ForeIconProperty =
            DependencyProperty.Register("ForeIcon", typeof(string), typeof(IconToggelButton),
                new PropertyMetadata(""));
        #endregion

        #region BackIcon
        public string BackIcon {
            get { return (string)GetValue(BackIconProperty); }
            set { SetValue(BackIconProperty, value); }
        }
        public static readonly DependencyProperty BackIconProperty =
            DependencyProperty.Register("BackIcon", typeof(string), typeof(IconToggelButton),
                new PropertyMetadata(""));
        #endregion

        #region ForeTip
        public string ForeToolTip {
            get { return (string)GetValue(ForeToolTipProperty); }
            set { SetValue(ForeToolTipProperty, value); }
        }
        public static readonly DependencyProperty ForeToolTipProperty =
            DependencyProperty.Register("ForeToolTip", typeof(string), typeof(IconToggelButton),
                new PropertyMetadata(""));
        #endregion

        #region BackToolTip
        public string BackToolTip {
            get { return (string)GetValue(BackToolTipProperty); }
            set { SetValue(BackToolTipProperty, value); }
        }
        public static readonly DependencyProperty BackToolTipProperty =
            DependencyProperty.Register("BackToolTip", typeof(string), typeof(IconToggelButton),
                new PropertyMetadata(""));
        #endregion

        #region ToolTipVisiblity
        public Visibility ToolTipVisiblity {
            get { return (Visibility)GetValue(ToolTipVisiblityProperty); }
            set { SetValue(ToolTipVisiblityProperty, value); }
        }
        public static readonly DependencyProperty ToolTipVisiblityProperty =
            DependencyProperty.Register("ToolTipVisiblity", typeof(Visibility), typeof(IconToggelButton),
                new PropertyMetadata(Visibility.Visible));
        #endregion

        public override void OnApplyTemplate() {

            base.OnApplyTemplate();
        }

        protected override void OnIsPressedChanged(DependencyPropertyChangedEventArgs e) {
            // Console.WriteLine(IsChecked);
            base.OnIsPressedChanged(e);
        }

        static IconToggelButton() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconToggelButton), new FrameworkPropertyMetadata(typeof(IconToggelButton)));
        }
    }
}
