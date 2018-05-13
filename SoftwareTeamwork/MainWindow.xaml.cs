using System;
using System.Collections.Generic;
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

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            Properties.Settings.Default.Upgrade();

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            NavigateTo.Target = MainFrame;
            MainFrame.NavigationService.Navigate(new Uri("AccountPage.xaml", UriKind.Relative));
        }

    }
}
