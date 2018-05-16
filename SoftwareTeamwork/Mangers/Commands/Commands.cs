using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SoftwareTeamwork {
    /// <summary>
    /// 关闭窗口
    /// </summary>
    public class CloseWinCommand : ICommand {
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) {
            return true;
        }
        public void Execute(object parameter) {
            ((AIWindow)parameter).Close();
        }
    }

    /// <summary>
    /// 退出程序
    /// </summary>
    public class ClsCommand : ICommand {
        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object parameter) {
            return true;
        }
        public void Execute(object parameter) {
            Application.Current.Shutdown();
        }
    }

    public class MCommand /* 移动命令 */ : ICommand {
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) {
            return true;
        }
        public void Execute(object parameter) {
            Win32APIs.ReleaseCapture();
            Win32APIs.SendMessage((IntPtr)parameter, Win32APIs.WM_SYSCOMMAND, (IntPtr)0xF012, IntPtr.Zero);
        }
    }

    public class OpenMainWindowCommand/* 打开主窗口*/ : ICommand {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            AIWindow a = parameter as AIWindow;
            if (a.Visibility.Equals(Visibility.Hidden))
                a.Show();
            else
                a.Hide();
        }
    }

    public class ChangeThemeCommand /*更换主题*/: ICommand {
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) {
            return true;
        }
        public void Execute(object parameter) {
            var dics = Application.Current.Resources.MergedDictionaries;
            dics[0].Source = new Uri(@"./Themes/BrightTheme.xaml", UriKind.Relative);
            OverallSettingManger.Instence.Theme = "BrightTheme.xaml";
        }
    }

    public class WindowCommand : ICommand /*命令基类*/ {
        public event EventHandler CanExecuteChanged;
        public delegate void Act(object para);
        private Act cAction;
        public event Act CAction {
            add { cAction = value; }
            remove { cAction -= value; }
        }
        public bool CanExecute(object parameter) {
            return true;
        }
        public void Execute(object parameter) {
                cAction(parameter);
        }
    }
}