using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Drawing = System.Drawing;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoftwareTeamwork {
    /// <summary>
    /// SettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingPage : Page {

        #region AreaIconFontSize
        public List<Double> AreaIconFontSize {
            get { return (List<Double>)GetValue(AreaIconFontSizeProperty); }
            set { SetValue(AreaIconFontSizeProperty, value); }
        }
        public static readonly DependencyProperty AreaIconFontSizeProperty =
            DependencyProperty.Register("AreaIconFontSize", typeof(List<Double>),
                typeof(SettingPage), new PropertyMetadata(new List<Double>()));
        #endregion

        public SettingPage() {
            for(int i = 4; i <= 24; i += 2) {
                AreaIconFontSize.Add(i);
            }
            InitializeComponent();
            Loaded += SettingPage_Loaded;
        }

        private void SettingPage_Loaded(object sender, RoutedEventArgs e) {
            AFontSize.ItemsSource = AreaIconFontSize;
            CFontSize.ItemsSource = AreaIconFontSize;

            InitControls();
        }

        private void InitControls() {
            ShowExit.IsChecked = !Properties.Settings.Default.IsExitDialogShow;
            ExitAction.IsChecked = Properties.Settings.Default.ExitAction;

            
        }

        private void Reset(object sender, RoutedEventArgs e) {
            QuestionDialog dialog = new QuestionDialog {
                Style = (Style)Application.Current.FindResource("QuestionDialog"),
                Context = "确定还原默认设置,包括用户数据?"
            };
            dialog.ShowDialog(Application.Current.MainWindow);

            if (dialog.DialogResult.Equals(true)) {
                OverallSettingManger.Instence.Reset();
            }
        }

        private void OtherSetting(object sender, RoutedEventArgs e) {
            switch (((Control)sender).Name) {
                case "ShowExit":
                    Properties.Settings.Default.IsExitDialogShow = !(bool)ShowExit.IsChecked;
                    break;
                case "ExitAction":
                    Properties.Settings.Default.ExitAction = (bool)ExitAction.IsChecked;
                    break;
            }
        }

        private void ChangeFontSize(object sender, SelectionChangedEventArgs e) {
            switch (((Control)sender).Name) {
                case "AFontSize":
                    OverallSettingManger.Instence.AFontSize = (double)AFontSize.SelectedValue;
                    break;
                case "CFontSize":
                    OverallSettingManger.Instence.CFontSize = (double)CFontSize.SelectedValue;
                    break;
            }
        }

        private void Color(object sender, RoutedEventArgs e) {
            ColorPicker picker;
            switch (((Control)sender).Name) {
                case "AColor":
                    picker = new ColorPicker();
                    picker.Style = Application.Current.FindResource("DefaultColorPicker") as Style;
                    picker.ShowDialogD(App.Current.MainWindow, OverallSettingManger.Instence.AFontColor);
                    
                    OverallSettingManger.Instence.AFontColor;
                    break;
                      
            }
        }

        private void AFontSize_KeyDown(object sender, KeyEventArgs e) {

        }
    }
}
