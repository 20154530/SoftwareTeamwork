using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
    public partial class MainWindow : AIWindow
    {
        #region NavigateCommand
        public WindowCommand NavigateTo {
            get { return (WindowCommand)GetValue(NavigateToProperty); }
            set { SetValue(NavigateToProperty, value); }
        }
        public static readonly DependencyProperty NavigateToProperty =
            DependencyProperty.Register("NavigateTo", typeof(WindowCommand), typeof(AccountPage),
                new PropertyMetadata(new WindowCommand()));
        #endregion

        protected override void OnClosing(CancelEventArgs e) {
            if (!Properties.Settings.Default.IsExitDialogShow) {
                if (Properties.Settings.Default.ExitAction) {
                    Hide();
                    e.Cancel = true;
                }
                else {
                    AreaIcon.Dispose();
                    Properties.Settings.Default.Save();
                    App.Current.Shutdown();
                }
            }
            else {
                ExitDialog dialog = new ExitDialog {
                    Style = (Style)Application.Current.FindResource("ExitDialogStyle"),
                    Context = "是否退出?"
                };
                dialog.ShowDialog(this);
                if (dialog.DialogResult == false) {
                    e.Cancel = true;
                    Properties.Settings.Default.ExitAction = false;
                    Properties.Settings.Default.IsExitDialogShow = true;
                }
                else {
                    if (Properties.Settings.Default.ExitAction) {
                        Hide();
                        e.Cancel = true;
                    }
                    else {
                        Properties.Settings.Default.Save();
                        Application.Current.Shutdown();
                    }
                }
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            NavigateTo.CAction += NavigateT;
            MainFrame.NavigationService.Navigate(new Uri("AccountPage.xaml", UriKind.Relative));
        }

        private void NavigateT(object para) {
            MainFrame.Navigate(new Uri((string)para,UriKind.Relative));
        }

        private void Instence_OpenSettingPage(object sender, EventArgs e) {
            Setting.IsChecked = true;
            MainFrame.Navigate(new Uri("SettingPage.xaml", UriKind.Relative));
        }

        public MainWindow() {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            OverallSettingManger.Instence.OpenSettingPage += Instence_OpenSettingPage;
            Properties.Settings.Default.Upgrade();
        }

    }
}
