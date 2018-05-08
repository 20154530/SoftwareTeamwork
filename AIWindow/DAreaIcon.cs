using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using Drawing = System.Drawing;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Reflection;
using System.Resources;

namespace AIWindow
{
    class DAreaIcon : UIElement, IDisposable
    {
        //DllImport
        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern bool DeleteObject(IntPtr hObject);
        private IntPtr ico = IntPtr.Zero;

        //NormalProperties
        private System.Timers.Timer PopupHidetimer;
        private System.Windows.Forms.NotifyIcon FlowIcon;
        private PrivateFontCollection pfc;
        private Font DisIconFont;
        private DPopup FlowIconPopup;

        //.NetProperties
        private AIWindow attachedWindow = null;
        public AIWindow AttachedWindow
        {
            get { return attachedWindow; }
            set { attachedWindow = value; }
        }

        private bool areaVisibility = false;
        public bool AreaVisibility
        {
            get { return areaVisibility; }
            set
            {
                areaVisibility = value;
                if (FlowIcon != null)
                    FlowIcon.Visible = value;
            }
        }

        private float fontsize = 13.2f;
        public float Fontsize
        {
            get { return fontsize; }
            set
            {
                fontsize = value;

            }
        }

        #region 初始化
        private void InitNotifyIcon()
        {
            InitRes();
            FlowIcon = new System.Windows.Forms.NotifyIcon
            {
                Visible = AreaVisibility,
                ContextMenu = new System.Windows.Forms.ContextMenu()
            };
            FlowIcon.MouseClick += FlowIcon_MouseClick;
            FlowIcon.MouseMove += FlowIcon_MouseMove;
            FlowIcon.MouseDoubleClick += FlowIcon_MouseDoubleClick;
            InitPopup();
        }

        private void InitRes()
        {
            byte[] myText = Properties.Resources.Rect;
            pfc = new PrivateFontCollection();
            IntPtr MeAdd = Marshal.AllocHGlobal(myText.Length);
            Marshal.Copy(myText, 0, MeAdd, myText.Length);
            pfc.AddMemoryFont(MeAdd, myText.Length);
        }

        private void InitPopup()
        {
            DisIconFont = new Font(pfc.Families[0], fontsize);
            DisIconFont.ToHfont();

            FlowIconPopup = (DPopup)Application.Current.FindResource("DPopup");
            FlowIconPopup.PlacementRectangle = new Rect(SystemParameters.WorkArea.Width - 305, SystemParameters.WorkArea.Height - 85,
                0, 0);
            FlowIconPopup.Child = (Border)Application.Current.FindResource("FlowContentPanel");
            FlowIconPopup.MouseMove += FlowIconPopup_MouseMove;
            FlowIconPopup.MouseLeave += FlowIconPopup_MouseLeave;
        }
        #endregion

        #region 计时器方法

        private void InitTimers()
        {
            //图标弹框淡出计时
            PopupHidetimer = new System.Timers.Timer(3000);
            PopupHidetimer.Elapsed += new ElapsedEventHandler(HidePopup);
        }

        private void HidePopup(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                FlowIconPopup.HidePopupAni();
            });
            PopupHidetimer.Enabled = false;
        }
        #endregion

        #region 事件方法

        private void FlowIconPopup_MouseLeave(object sender, MouseEventArgs e)
        {
            PopupHidetimer.Enabled = true;
        }

        private void FlowIconPopup_MouseMove(object sender, MouseEventArgs e)
        {
            PopupHidetimer.Enabled = false;
        }

        private void FlowIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Right:
                    DContextMenu d = (DContextMenu)Application.Current.FindResource("DcontextMenu");
                    d.CommandParameter = attachedWindow;
                    d.IsOpen = true;
                    break;
                case System.Windows.Forms.MouseButtons.Left:
                    if (!FlowIconPopup.IsOpen)
                        FlowIconPopup.ShowPopupAni();
                    PopupHidetimer.Enabled = true;
                    break;
            }
        }

        private void FlowIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {


        }

        private void FlowIcon_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {

        }
        #endregion

        #region GraphUpdata
        //托盘画图
        private Icon GetImageSourceByText(String Inf)
        {
            Drawing.Image bufferedimage;
            if (ico == IntPtr.Zero)
                bufferedimage = new Bitmap(35, 30, PixelFormat.Format32bppArgb);
            else
                bufferedimage = Bitmap.FromHicon(ico);

            Graphics g = Graphics.FromImage(bufferedimage);
            g.Clear(Color.FromArgb(0, 255, 255, 255));
            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
            Pen pen = new Pen(Color.FromArgb(255, 255, 255, 255), 1f);
            g.DrawString(Inf, DisIconFont, pen.Brush, new Drawing.Point(0, 1), new StringFormat() { });
            ico = (bufferedimage as Bitmap).GetHicon();

            g.Dispose();
            return Icon.FromHandle(ico);
        }
        #endregion

        #region 公共方法
        public void UpdataIconByStr(String s)
        {
            FlowIcon.Icon = GetImageSourceByText(s);
        }

        public void Dispose()
        {
            FlowIcon.Dispose();
            PopupHidetimer.Dispose();
        }
        #endregion

        //构造函数
        public DAreaIcon(AIWindow a)
        {
            AttachedWindow = a;
            InitNotifyIcon();
            InitTimers();
        }
    }
}
