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
                ErrorMessageService.Instence.ShowError(this, "请输入用户名和密码");
            else {
                XmlAnalyze.UpdateNodeValue("NEUIpgw", "username", IPGWAccount.Text);
                XmlAnalyze.UpdateNodeValue("NEUIpgw", "password", IPGWPassword.Password);
            }
        }

        private void Identify(object sender, RoutedEventArgs e) {
            if (JWAccount.Text.Equals("") || JWPassword.Password.Equals(""))
                ErrorMessageService.Instence.ShowError(this, "请输入用户名和密码");
            else {
                if (LoginAgent.Instence.SetInfset(XmlAnalyze.GetInfWithName("NEUZhjw")) == -1) 
                    return;
                JWIdentifyImage.Source = LoginAgent.Instence.GetVerify();
                JWIdentifyImage.Visibility = Visibility.Visible;
            }
        }

        private void SaveNEUJW(object sender, RoutedEventArgs e) {
            if (JWAccount.Text.Equals("") || JWPassword.Password.Equals("") || JWIdentifyCode.Equals(""))
                ErrorMessageService.Instence.ShowError(this, "请输入用户名、密码、验证码");
            else {
                XmlAnalyze.UpdateNodeValue("NEUIpgw", "username", JWAccount.Text);
                XmlAnalyze.UpdateNodeValue("NEUIpgw", "password", JWPassword.Password);
                XmlAnalyze.UpdateNodeValue("NEUIpgw", "username", JWIdentifyCode.Text);
            }
        }
    }
}
