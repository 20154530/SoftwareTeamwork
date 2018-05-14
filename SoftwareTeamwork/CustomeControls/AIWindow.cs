using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;

namespace SoftwareTeamwork {
    public class AIWindow : Window
    {
        #region ExtendToTitleBar
        public bool ExtendToTitleBar {
            get { return (bool)GetValue(ExtendToTitleBarProperty); }
            set { SetValue(ExtendToTitleBarProperty, value); }
        }
        public static readonly DependencyProperty ExtendToTitleBarProperty =
            DependencyProperty.Register("ExtendToTitleBar", typeof(bool), typeof(AIWindow), new PropertyMetadata(false));
        #endregion

        #region AreaIcon
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

        static AIWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AIWindow), new FrameworkPropertyMetadata(typeof(AIWindow)));
        }
    }
}
