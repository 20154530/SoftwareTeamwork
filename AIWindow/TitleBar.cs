using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AIWindow
{
    public class TitleBar : Control
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public static IntPtr _Handle;

        #region AttachedAIWindowProperty
        public static readonly DependencyProperty AttachedWindowProperty = DependencyProperty.Register("AttachedWindow", typeof(AIWindow), typeof(TitleBar),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(AttachedWindowChangedCallback)));
        public AIWindow AttachedWindow
        {
            get { return (AIWindow)GetValue(AttachedWindowProperty); }
            set { SetValue(AttachedWindowProperty, value); }
        }
        private static void AttachedWindowChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {
            _Handle = new WindowInteropHelper(sender.GetValue(AttachedWindowProperty) as Window).Handle;
        }
        #endregion

        #region CommandProperty
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(TitleBar)
            );
        public ICommand Command
        {
            get { return (ICommand)this.GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        #endregion

        #region CommandParameter
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(Object), typeof(TitleBar));
        public Object CommandParameter
        {
            get { return (Object)this.GetValue(CommandParameterProperty); }
            set { this.SetValue(CommandParameterProperty, value); }
        }
        #endregion

        #region CommandTarget
        public static readonly DependencyProperty CommandTargetProperty = DependencyProperty.Register("CommandTarget", typeof(IInputElement), typeof(TitleBar));
        public IInputElement CommandTarget
        {
            get { return (IInputElement)GetValue(CommandTargetProperty); }
            set { SetValue(CommandTargetProperty, value); }
        }
        #endregion

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            RoutedCommand rcmd = Command as RoutedCommand;
            if (rcmd != null)
            {
                if (rcmd.CanExecute(CommandParameter, CommandTarget))
                    rcmd.Execute(CommandParameter, CommandTarget);
            }
            else
            {
                if (Command != null)
                    if (Command.CanExecute(CommandParameter))
                        Command.Execute(CommandParameter);
            }
        }

        static TitleBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TitleBar), new FrameworkPropertyMetadata(typeof(TitleBar)));
        }
    }
}
