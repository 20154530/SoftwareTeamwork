using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SoftwareTeamwork
{
    public class ClsCommand /* 关闭命令 */ : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }

    public class MCommand /* 移动命令 */ : ICommand
    {
        private const int WM_SYSCOMMAND = 0x112;

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            ReleaseCapture();
            TitleBar.SendMessage(TitleBar._Handle, WM_SYSCOMMAND, (IntPtr)0xF012, IntPtr.Zero);
        }
    }

    public class OpenMainWindowCommand/* 打开主窗口*/ : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            AIWindow a = parameter as AIWindow;
            if (a.Visibility.Equals(Visibility.Hidden))
                a.Show();
            else
                a.Hide();
        }
    }

    public class ChangeThemeCommand /*更换主题*/: ICommand {
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            var dics = Application.Current.Resources.MergedDictionaries;
            //ResourceDictionary skinDict = Application.LoadComponent(new Uri(@"./Themes/BrightTheme.xaml", UriKind.Relative)) as ResourceDictionary;
            //dics.RemoveAt(0);
            //dics.Insert(0, skinDict);
            OverallSettingManger.Instence.Theme = "BrightTheme.xaml";
            dics[0].Source = new Uri(@"./Themes/BrightTheme.xaml", UriKind.Relative);
        }
    }
}
