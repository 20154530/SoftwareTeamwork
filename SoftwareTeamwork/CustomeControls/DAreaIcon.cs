using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Drawing = System.Drawing;

namespace SoftwareTeamwork
{
    public class DAreaIcon : UIElement, IDisposable
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

        //.NetProperties

        private float fontsize = (float)Properties.Settings.Default.AreaIconFontSize;
        public float Fontsize
        {
            get { return fontsize; }
            set { fontsize = value; }
        }

        private Drawing.Color fontColor = Properties.Settings.Default.AreaIconColor;
        public Drawing.Color FontColor {
            get => fontColor;
            set { fontColor = value; }
        }

        #region AttachedWindow
        public object AttachedWindow {
            get { return (object)GetValue(AttachedWindowProperty); }
            set { SetValue(AttachedWindowProperty, value); }
        }
        public static readonly DependencyProperty AttachedWindowProperty =
            DependencyProperty.Register("AttachedWindow", typeof(object), typeof(DAreaIcon),
                new PropertyMetadata(null,new PropertyChangedCallback(OnAttachedWindowChanged)));
        private static void OnAttachedWindowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DAreaIcon dai = (DAreaIcon)d;
            dai.Dcontextmenu.CommandParameter = e.NewValue;
        }
        #endregion

        #region AreaVisibility
        public bool AreaVisibility {
            get { return (bool)GetValue(AreaVisibilityProperty); }
            set { SetValue(AreaVisibilityProperty, value); }
        }
        public static readonly DependencyProperty AreaVisibilityProperty =
            DependencyProperty.Register("AreaVisibility", typeof(bool), typeof(DAreaIcon),
                new PropertyMetadata(false,new PropertyChangedCallback(OnAreaVisibilityChanged)));
        private static void OnAreaVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DAreaIcon dai = (DAreaIcon)d;
            dai.FlowIcon.Visible = (bool)e.NewValue;
        }
        #endregion

        #region Dcontextmenu
        public DContextMenu Dcontextmenu {
            get { return (DContextMenu)GetValue(DcontextmenuProperty); }
            set { SetValue(DcontextmenuProperty, value); }
        }
        public static readonly DependencyProperty DcontextmenuProperty =
            DependencyProperty.Register("Dcontextmenu", typeof(DContextMenu), typeof(DAreaIcon), 
                new PropertyMetadata(null,new PropertyChangedCallback(OnDcontextmenuChanged)));
        private static void OnDcontextmenuChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DAreaIcon dai = (DAreaIcon)d;
            dai.Dcontextmenu.CommandParameter = dai.AttachedWindow;
        }
        #endregion

        #region FlowIconPopup
        public FluxPopup FlowIconPopup {
            get { return (FluxPopup)GetValue(FlowIconPopupProperty); }
            set { SetValue(FlowIconPopupProperty, value); }
        }
        public static readonly DependencyProperty FlowIconPopupProperty =
            DependencyProperty.Register("FlowIconPopup", typeof(FluxPopup), typeof(DAreaIcon),
                new PropertyMetadata(null,new PropertyChangedCallback(OnFlowIconPopupChanged)));
        private static void OnFlowIconPopupChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DAreaIcon dai = (DAreaIcon)d;
            if (e.NewValue != null) 
                dai.InitPopup();
        }
        #endregion
        
        #region 初始化
        private void InitNotifyIcon()
        {
            InitRes();
            FlowIcon = new System.Windows.Forms.NotifyIcon
            {
                Visible = AreaVisibility,
                ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip()
            };
            FlowIcon.MouseClick += FlowIcon_MouseClick;
            FlowIcon.MouseMove += FlowIcon_MouseMove;
            FlowIcon.MouseDoubleClick += FlowIcon_MouseDoubleClick;
        }

        private void InitRes() {
            byte[] myText = null;
            IntPtr MeAdd = IntPtr.Zero;

            myText = (byte[])Properties.Resources.ResourceManager.GetObject("Rect");
            pfc = new PrivateFontCollection();
            MeAdd = Marshal.AllocHGlobal(myText.Length);
            Marshal.Copy(myText, 0, MeAdd, myText.Length);
            pfc.AddMemoryFont(MeAdd, myText.Length);

            DisIconFont = new Font(pfc.Families[0], fontsize);
        }

        private void InitPopup()
        {
            FlowIconPopup.PlacementRectangle = 
                new Rect(SystemParameters.WorkArea.Width -5, 
                SystemParameters.WorkArea.Height - 5, 0, 0);
            FlowIconPopup.Title = "流量信息";
            FlowIconPopup.SetIconPathByPercentAngle(Properties.Settings.Default.LastFluxInfo);
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
            if (PopupHidetimer != null)
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
                    Dcontextmenu.IsOpen = true;
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

        #region GraphUpdata -- 托盘画图
        private Icon GetImageSourceByText(String Inf)
        {
            int size = 30;
            Drawing.Image bufferedimage;
            if (ico == IntPtr.Zero)
                bufferedimage = new Bitmap(size, size, Drawing.Imaging.PixelFormat.Format32bppArgb);
            else
                bufferedimage = Bitmap.FromHicon(ico);

            Graphics g = Graphics.FromImage(bufferedimage);
            g.Clear(Drawing.Color.FromArgb(0, 255, 255, 255));
            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
            Drawing.Pen pen = new Drawing.Pen(fontColor, 1f);
            SizeF infsize = g.MeasureString(Inf, DisIconFont);
            g.DrawString(Inf, DisIconFont, pen.Brush,
                new Drawing.Point((int)((size - infsize.Width) / 2), (int)((size - infsize.Height) / 2)));
            ico = (bufferedimage as Bitmap).GetHicon();

            bufferedimage.Dispose();
            g.Dispose();

            return Icon.FromHandle(ico);
        }
        #endregion

        #region 公共方法

        /// <summary>
        /// 更新图标
        /// </summary>
        /// <param name="s">以百分比表示的字符数字 </param>
        public void UpdataIconByStr(String s)
        {
            FlowIcon.Icon = GetImageSourceByText(s);
        }

        #endregion

        #region 设置更新
        private void Instence_OnAFontColorChanged(object sender, EventArgs e) {
            fontColor = (Drawing.Color)sender;
            UpdataIconByStr(Math.Round(Properties.Settings.Default.LastFluxInfo).ToString());
        }

        private void Instence_OnAFontSizeChanged(object sender, EventArgs e) {
            fontsize = (float)(double)sender;
            DisIconFont = new Font(pfc.Families[0], fontsize);
            UpdataIconByStr(Math.Round(Properties.Settings.Default.LastFluxInfo).ToString());
        }

        private void Instence_OnFluxUpdate(object sender, EventArgs e) {
            UpdataIconByStr(Math.Round((double)sender-1).ToString());
        }
        #endregion

        public DAreaIcon() {
            OverallSettingManger.Instence.OnFluxUpdate += Instence_OnFluxUpdate;
            OverallSettingManger.Instence.OnAFontSizeChanged += Instence_OnAFontSizeChanged;
            OverallSettingManger.Instence.OnAFontColorChanged += Instence_OnAFontColorChanged;
            App.Current.MainWindow.Closing += MainWindow_Closing;
            fontsize = (float)Properties.Settings.Default.AreaIconFontSize;
            InitNotifyIcon();
            InitTimers();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            FlowIconPopup.IsOpen = false;
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: 释放托管状态(托管对象)。
                }
                FlowIcon.Dispose();
                PopupHidetimer.Dispose();
                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
