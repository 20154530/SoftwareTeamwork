using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;

namespace SoftwareTeamwork {
    public class AIWindow : Window
    {
        private bool IsExiting = false;
        #region 托盘图标
        public DAreaIcon AreaIcon {
            get { return (DAreaIcon)GetValue(AreaIconProperty); }
            set { SetValue(AreaIconProperty, value); }
        }
        public static readonly DependencyProperty AreaIconProperty =
            DependencyProperty.Register("AreaIcon", typeof(DAreaIcon), typeof(AIWindow), new PropertyMetadata(null));
        #endregion

        #region override
        protected override void OnInitialized(EventArgs e)
        {
            AreaIcon.AttachedWindow = this;
            base.OnInitialized(e);
        }
        #endregion

        #region 截获消息循环
        private void WSInitialized(object sender, EventArgs e)
        {
            (PresentationSource.FromVisual((Visual)sender) as HwndSource).AddHook(new HwndSourceHook(WndProc));
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            int a = wParam.ToInt32();
            //switch (msg)
            //{

            //}
            return IntPtr.Zero;
        }
        #endregion

        #region 控件初始化

        #endregion

        #region 部件事件

        #endregion

        protected override void OnClosing(CancelEventArgs e)
        {
            if (IsExiting)
                return;
            else {
                if (!Properties.Settings.Default.IsExitDialogShow) {
                    if (Properties.Settings.Default.ExitAction) {
                        Hide();
                        return;
                    }
                    else {
                        AreaIcon.Dispose();
                        App.Current.Shutdown();
                        return;
                    }
                }
                else {
                    IsExiting = true;
                    ExitDialog dialog = new ExitDialog {
                        Template = (ControlTemplate)Application.Current.FindResource("ExitDialogStyle"),
                        Context = "是否退出?"
                    };
                    dialog.ShowDialog(this);
                    e.Cancel = dialog.DialogResult == false;
                    IsExiting = false;
                    return;
                }
            }
        }

        static AIWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AIWindow), new FrameworkPropertyMetadata(typeof(AIWindow)));
        }
    }
}
