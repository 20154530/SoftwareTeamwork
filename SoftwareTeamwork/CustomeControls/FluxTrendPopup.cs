using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SoftwareTeamwork {
    public class FluxTrendPopup : DPopup {

        #region MousePoint.X
        public double PosX {
            get { return (double)GetValue(PosXProperty); }
            set { SetValue(PosXProperty, value); }
        }
        public static readonly DependencyProperty PosXProperty =
            DependencyProperty.Register("PosX", typeof(double), typeof(FluxTrendPopup),
                new PropertyMetadata(0.0));
        #endregion

        #region MousePoint.Y
        public double PosY {
            get { return (double)GetValue(PosYProperty); }
            set { SetValue(PosYProperty, value); }
        }
        public static readonly DependencyProperty PosYProperty =
            DependencyProperty.Register("PosY", typeof(double), typeof(FluxTrendPopup),
                new PropertyMetadata(0.0));
        #endregion

        #region DataGroup 
        /// <summary>
        /// 流量数据坐标信息
        /// </summary>
        public FluxTrendGroup DataGroup {
            get { return (FluxTrendGroup)GetValue(DataGroupProperty); }
            set { SetValue(DataGroupProperty, value); }
        }
        public static readonly DependencyProperty DataGroupProperty =
            DependencyProperty.Register("DataGroup", typeof(FluxTrendGroup), typeof(FluxTrendPopup),
                new PropertyMetadata(new FluxTrendGroup(), new PropertyChangedCallback(OnDataGroupChanged)));
        private static void OnDataGroupChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((FluxTrendGroup)e.NewValue).GetFluxData();
        }
        #endregion

        #region CursorVis
        public Visibility CursorVis {
            get { return (Visibility)GetValue(CursorVisProperty); }
            set { SetValue(CursorVisProperty, value); }
        }
        public static readonly DependencyProperty CursorVisProperty =
            DependencyProperty.Register("CursorVis", typeof(Visibility), typeof(FluxTrendPopup),
                new PropertyMetadata(Visibility.Collapsed));
        #endregion

        #region
        public WindowCommand Update {
            get { return (WindowCommand)GetValue(UpdateProperty); }
            set { SetValue(UpdateProperty, value); }
        }
        public static readonly DependencyProperty UpdateProperty =
            DependencyProperty.Register("Update", typeof(WindowCommand), 
                typeof(FluxTrendPopup), new PropertyMetadata(new WindowCommand()));
        #endregion

        #region MouseAactions
        protected override void OnMouseMove(MouseEventArgs e) {
            PosX = e.GetPosition(null).X - 32;
            PosY = e.GetPosition(null).Y - 20;
            base.OnMouseMove(e);
        }

        protected override void OnMouseEnter(MouseEventArgs e) {
            CursorVis = Visibility.Visible;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e) {
            CursorVis = Visibility.Collapsed;
            base.OnMouseLeave(e);
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e) {
            base.OnPreviewMouseLeftButtonDown(e);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e) {
            base.OnMouseLeftButtonUp(e);
        }
        #endregion

        private void Update_CAction(object para) {
            throw new NotImplementedException();
        }

        #region
        protected override void OnOpened(EventArgs e) {           
            base.OnOpened(e);
        }

        protected override void OnStyleChanged(Style oldStyle, Style newStyle) {
            base.OnStyleChanged(oldStyle, newStyle);
        }

        private void HidePopup(object sender, ElapsedEventArgs e) {
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (ThreadStart)delegate () {
                this.HidePopupAni();
            });
        }
        #endregion

        public FluxTrendPopup() {
            DataGroup = XmlHelper.GetFluxTrendGroup(DateTime.Now.AddDays(-6), DateTime.Now);
            Update.CAction += Update_CAction;
        }
    }
}