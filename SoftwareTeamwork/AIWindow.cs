using System;
using System.ComponentModel;
using System.Windows;
using System.Timers;
using System.Windows.Media;
using System.Windows.Interop;

namespace SoftwareTeamwork
{
    public class AIWindow : Window
    {
        private Random random;
        private DAreaIcon AreaIcon;
        //Timers
        private System.Timers.Timer IconUpdatatimer;

        #region override
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            InitTimers();
            random = new Random();
            AreaIcon = new DAreaIcon
            {
                AreaVisibility = true
            };
            SourceInitialized += new EventHandler(WSInitialized);
        }
        #endregion

        //截获消息循环
        private void WSInitialized(object sender, EventArgs e)
        {
            (PresentationSource.FromVisual((Visual)sender) as HwndSource).AddHook(new HwndSourceHook(WndProc));
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            int a = wParam.ToInt32();
            switch (msg)
            {
                case 5:
                    switch (a)
                    {
                        case 2:
                            
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }


        #region 控件初始化
        private void InitTimers()
        {
            //图标刷新计时器
            IconUpdatatimer = new System.Timers.Timer(1000);
            IconUpdatatimer.Elapsed += new ElapsedEventHandler(Updatetext);
            IconUpdatatimer.Enabled = true;
        }
        #endregion

        #region 部件事件

        private void Updatetext(object source, ElapsedEventArgs e)
        {
            try
            {
                AreaIcon.UpdataIconByStr(random.Next(0,100).ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        #endregion

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            IconUpdatatimer.Close();
            AreaIcon.Dispose();
        }

        static AIWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AIWindow), new FrameworkPropertyMetadata(typeof(AIWindow)));
        }
    }
}
