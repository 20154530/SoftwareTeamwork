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

namespace SoftwareTeamwork {
    /// <summary>
    /// SettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingPage : Page {

        #region AreaIconFontSize
        public ObservableCollection<double> AreaIconFontSize {
            get { return (ObservableCollection<double>)GetValue(AreaIconFontSizeProperty); }
            set { SetValue(AreaIconFontSizeProperty, value); }
        }
        public static readonly DependencyProperty AreaIconFontSizeProperty =
            DependencyProperty.Register("AreaIconFontSize", typeof(ObservableCollection<double>),
                typeof(SettingPage), new PropertyMetadata(new ObservableCollection<double>()));
        #endregion

        public SettingPage() {
            for(int i = 4; i <= 72; i += 4) {
                AreaIconFontSize.Add(i);
            }
            InitializeComponent();
            Loaded += SettingPage_Loaded;
        }

        private void SettingPage_Loaded(object sender, RoutedEventArgs e) {
            AFontSize.ItemsSource = AreaIconFontSize;
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

        private void FontSize_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            switch (((Control)sender).Name) {
                case "AFontSize":
                    Properties.Settings.Default.AreaIconFontSize = (double)AFontSize.SelectedValue;
                    OverallSettingManger.Instence.AFontSize = (double)AFontSize.SelectedValue;
                    break;
                case "CFontSize":
                    break;
            }
        }

        private void Color(object sender, RoutedEventArgs e) {

        }
    }
}
