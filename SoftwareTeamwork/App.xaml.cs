using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace SoftwareTeamwork
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private static string DllPath = "Libs";
        public static string RootPath = AppDomain.CurrentDomain.BaseDirectory;

       
        protected override void OnExit(ExitEventArgs e) {
            SoftwareTeamwork.Properties.Settings.Default.Save();
            base.OnExit(e);
        }

        private void Application_Startup(object sender, StartupEventArgs e) {
            App.Current.SessionEnding += Current_SessionEnding;
            MainWindow window = new MainWindow();
            window.Show();
            if (!e.Args.Length.Equals(0))
                window.Hide();
        }

        private void Current_SessionEnding(object sender, SessionEndingCancelEventArgs e) {
            SoftwareTeamwork.Properties.Settings.Default.Save();
        }
    }
}
