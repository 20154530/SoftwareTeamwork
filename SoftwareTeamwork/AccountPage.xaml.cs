using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SoftwareTeamwork {

    public partial class AccountPage : Page {

        public AccountPage() {
            InitializeComponent();
            Loaded += AccountPage_Loaded;
        }

        private void AccountPage_Loaded(object sender, RoutedEventArgs e) {
            InitControls();
        }

        private void InitControls() {

            IPGWAccount.Text = Properties.Settings.Default.IPGWA;
            IPGWPassword.Password = Properties.Settings.Default.IPGWP;
            JWAccount.Text = Properties.Settings.Default.JWA;
            JWPassword.Password = Properties.Settings.Default.JWP;

            IPGWAccount.IsEnabled = !Properties.Settings.Default.IPGWF;
            IPGWPassword.IsEnabled = !Properties.Settings.Default.IPGWF;
            Flux15.IsEnabled = !Properties.Settings.Default.IPGWF;
            Flux20.IsEnabled = !Properties.Settings.Default.IPGWF;
            Flux15.IsChecked = !Properties.Settings.Default.FluxPackage;
            Flux20.IsChecked = Properties.Settings.Default.FluxPackage;
            JWIdentifyCodeLayer.Visibility = Properties.Settings.Default.JWF ? Visibility.Collapsed : Visibility.Visible;
            JWAccount.IsEnabled = !Properties.Settings.Default.JWF;
            JWPassword.IsEnabled = !Properties.Settings.Default.JWF;

        }

        private void SaveIPGW(object sender, RoutedEventArgs e) {
            if (IPGWAccount.Text.Equals("") || IPGWPassword.Password.Equals("")) 
                MessageService.Instence.ShowError(App.Current.MainWindow, "请输入用户名和密码");
            else 
                SaveIPGW();
        }

        private async void SaveIPGW(){
            if (!Properties.Settings.Default.IPGWF) {
                Properties.Settings.Default.IPGWA = IPGWAccount.Text;
                Properties.Settings.Default.IPGWP = IPGWPassword.Password;
                Properties.Settings.Default.IPGWF = true;
                Flux15.IsEnabled = !Properties.Settings.Default.IPGWF;
                Flux20.IsEnabled = !Properties.Settings.Default.IPGWF;
                Flux15.Visibility = Properties.Settings.Default.FluxPackage ? Visibility.Collapsed : Visibility.Visible;
                Flux20.Visibility = Properties.Settings.Default.FluxPackage ? Visibility.Visible : Visibility.Collapsed;
                IPGWAccount.IsEnabled = false;
                IPGWPassword.IsEnabled = false;
                string name, password;
                name = IPGWAccount.Text;
                password = IPGWPassword.Password;
                Task t = new Task(() => {
                    Dictionary<string, string> dc = new Dictionary<string, string> {
                    { "username", name },
                    { "password", password },
                };
                    XmlHelper.UpdateWebNodeValue("NEUIpgw", dc);
                });
                t.Start();
                await t;
            }
        }

        private void ChangeIPGW(object sender, RoutedEventArgs e) {
            Properties.Settings.Default.IPGWF = false;
            IPGWAccount.IsEnabled = true;
            IPGWPassword.IsEnabled = true;
            Flux15.IsEnabled = true;
            Flux20.IsEnabled = true;
            Flux15.Visibility = Visibility.Visible;
            Flux20.Visibility = Visibility.Visible;
        }

        private void Identify(object sender, RoutedEventArgs e) {
            if (JWAccount.Text.Equals("") || JWPassword.Password.Equals(""))
                MessageService.Instence.ShowError(App.Current.MainWindow, "请输入用户名和密码");
            else {
                if (LoginAgent.Instence.SetInfset(XmlHelper.GetInfWithName("NEUZhjw")) == -1) 
                    return;
                JWIdentifyImage.Source = LoginAgent.Instence.GetVerify();
                JWIdentifyImage.Visibility = Visibility.Visible;
            }
        }

        private void SaveNEUJW(object sender, RoutedEventArgs e) {
            if (JWAccount.Text.Equals("") || JWPassword.Password.Equals("") || (JWIdentifyCode.Text.Equals("") && !Properties.Settings.Default.JWF))
                MessageService.Instence.ShowError(App.Current.MainWindow, "请输入用户名、密码、验证码");
            else
                SaveNEUJW();
        }

        private async void SaveNEUJW() {
            if (!Properties.Settings.Default.JWF) {
                Properties.Settings.Default.JWA = JWAccount.Text;
                Properties.Settings.Default.JWP = JWPassword.Password;
                Properties.Settings.Default.JWF = true;
                JWAccount.IsEnabled = false;
                JWPassword.IsEnabled = false;
                JWIdentifyCodeLayer.Visibility = Visibility.Collapsed;
                string name, password, id;
                name = JWAccount.Text;
                password = JWPassword.Password;
                id = JWIdentifyCode.Text;
                Task t = new Task(() => {
                    Dictionary<string, string> dc = new Dictionary<string, string> {
                    { "WebUserNO", name },
                    { "Password", password },
                    { "Agnomen", id }
                     };
                    XmlHelper.UpdateWebNodeValue("NEUZhjw", dc);
                });
                t.Start();
                await t;
                LoginAgent.Instence.Post("NEUZhjw");
            }
        }

        private void ChangeNEUJW(object sender, RoutedEventArgs e) {
            Properties.Settings.Default.JWF = false;
            JWIdentifyCodeLayer.Visibility = Visibility.Visible;
            JWAccount.IsEnabled = true;
            JWPassword.IsEnabled = true;

            XmlHelper.DeleteWebNode("NEUZhjw", new HashSet<string>() { "cookie" });
        }

        private void FluxKindChoice(object sender, RoutedEventArgs e) {
            switch (((Control)sender).Name) {
                case "Flux15":
                    Properties.Settings.Default.FluxPackage = false;
                    break;
                case "Flux20":
                    Properties.Settings.Default.FluxPackage = true;
                    break;
            }
        }
    }
}