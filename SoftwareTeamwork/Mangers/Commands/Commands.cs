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
    /// <summary>
    /// 关闭窗口
    /// </summary>
    public class CloseWinCommand : ICommand {
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            ((AIWindow)parameter).Close();
        }
    }

    /// <summary>
    /// 退出程序
    /// </summary>
    public class ClsCommand : ICommand
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
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            ReleaseCapture();
            SendMessage((IntPtr)parameter, WM_SYSCOMMAND, (IntPtr)0xF012, IntPtr.Zero);
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
            dics[0].Source = new Uri(@"./Themes/BrightTheme.xaml", UriKind.Relative);
            OverallSettingManger.Instence.Theme = "BrightTheme.xaml";
        }
    }

    /// <summary>
    /// 用于模板化的弹出式对话框控件中的响应控件向对话框传递消息
    /// </summary>
    public class DialogInvokCommand : ICommand {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            
        }
    }
}
