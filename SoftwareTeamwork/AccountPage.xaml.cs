using System;
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
            
        }

        private void SaveIPGW(object sender, RoutedEventArgs e) {
            if (IPGWAccount.Text.Equals("") || IPGWPassword.Password.Equals(""))
                ErrorMessageService.ShowError(this, "请输入用户名与密码");
            else {
                XmlAnalyze.UpdateNodeValue("NEUIpgw", "username", IPGWAccount.Text);
                XmlAnalyze.UpdateNodeValue("NEUIpgw", "password", IPGWPassword.Password);
            }
        }

        private void Identify(object sender, RoutedEventArgs e) {
            if (JWAccount.Text.Equals("") || JWPassword.Password.Equals(""))
                return;
            else {
                //JWIdentifyImage.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => {
                LoginAgent.Instence.SetInfset(XmlAnalyze.GetInfWithName("NEUZhjw"));
                JWIdentifyImage.Source = LoginAgent.Instence.GetVerify();
                //}));
            }
        }

        private void SaveNEUJW(object sender, RoutedEventArgs e) {
            if (JWAccount.Text.Equals("") || JWPassword.Password.Equals("") || JWIdentifyCode.Equals(""))
                return;
            else {
                XmlAnalyze.UpdateNodeValue("NEUIpgw", "username", JWAccount.Text);
                XmlAnalyze.UpdateNodeValue("NEUIpgw", "password", JWPassword.Password);
                XmlAnalyze.UpdateNodeValue("NEUIpgw", "username", JWIdentifyCode.Text);
            }
        }
    }
}
