﻿using System;
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

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args) {
            AssemblyName assemblyName = new AssemblyName(args.Name);
            return Assembly.LoadFrom(Path.Combine(RootPath, DllPath));
        }

        private void Application_Startup(object sender, StartupEventArgs e) {
            MainWindow window = new MainWindow();
            window.Show();
            if (!e.Args.Length.Equals(0))
                window.Hide();
        }
    }
}
