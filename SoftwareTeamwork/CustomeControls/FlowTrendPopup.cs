using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SoftwareTeamwork {
    public class FlowTrendPopup : DPopup {

        #region MousePoint.X
        public double PosX {
            get { return (double)GetValue(PosXProperty); }
            set { SetValue(PosXProperty, value); }
        }
        public static readonly DependencyProperty PosXProperty =
            DependencyProperty.Register("PosX", typeof(double), typeof(FlowTrendPopup),
                new PropertyMetadata(0.0));
        #endregion

        #region MousePoint.Y
        public double PosY {
            get { return (double)GetValue(PosYProperty); }
            set { SetValue(PosYProperty, value); }
        }
        public static readonly DependencyProperty PosYProperty =
            DependencyProperty.Register("PosY", typeof(double), typeof(FlowTrendPopup),
                new PropertyMetadata(0.0));
        #endregion

        #region DataGroup 
        /// <summary>
        /// 流量数据坐标信息
        /// </summary>
        public FlowTrendGroup DataGroup {
            get { return (FlowTrendGroup)GetValue(DataGroupProperty); }
            set { SetValue(DataGroupProperty, value); }
        }
        public static readonly DependencyProperty DataGroupProperty =
            DependencyProperty.Register("DataGroup", typeof(FlowTrendGroup), typeof(FlowTrendPopup),
                new PropertyMetadata(new FlowTrendGroup(), new PropertyChangedCallback(OnDataGroupChanged)));
        private static void OnDataGroupChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((FlowTrendGroup)e.NewValue).GetFlowData();
        }
        #endregion

        #region CursorVis
        public Visibility CursorVis {
            get { return (Visibility)GetValue(CursorVisProperty); }
            set { SetValue(CursorVisProperty, value); }
        }
        public static readonly DependencyProperty CursorVisProperty =
            DependencyProperty.Register("CursorVis", typeof(Visibility), typeof(FlowTrendPopup),
                new PropertyMetadata(Visibility.Collapsed));
        #endregion

        protected override void OnStyleChanged(Style oldStyle, Style newStyle) {
            base.OnStyleChanged(oldStyle, newStyle);
        }

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

        public FlowTrendPopup() {
            // Cursor = new Cursor(new MemoryStream(Properties.Resources.Arrow));
        }

    }

    public class FlowTrendGroup {
        public FlowInfo[] FlowInfos { get; set; }
        public double[] ActrualData { get; set; }
        public double[] VTicks { get; set; } 

        public FlowTrendGroup() {
            FlowInfos = new FlowInfo[7] {
                    new FlowInfo { FlowData = 4, InfoTime = new DateTime(0, 0) },
                    new FlowInfo { FlowData = 6, InfoTime = new DateTime(0, 0) },
                    new FlowInfo { FlowData = 6, InfoTime = new DateTime(0, 0) },
                    new FlowInfo { FlowData = 10, InfoTime = new DateTime(0, 0) },
                    new FlowInfo { FlowData = 6, InfoTime = new DateTime(0, 0) },
                    new FlowInfo { FlowData = 6, InfoTime = new DateTime(0, 0) },
                    new FlowInfo { FlowData = 4, InfoTime = new DateTime(0, 0) },
            };
            ActrualData = new double[7];
            VTicks = new double[6];
            TransformToNode();
        }

        public void GetFlowData() {

            TransformToNode();
        }

        private void TransformToNode() {
            double est = 1;
            for (int i = 0; i < 7; i++) {
                ActrualData[i] = FlowInfos[i].FlowData;//备份原有数据
                est = FlowInfos[i].FlowData > est ? FlowInfos[i].FlowData : est;
            }//找最大流量

            est = est * 1.2;//画刻度
            for(int i = 0; i < 6; i++) 
                VTicks[i] = est / 6 * (i + 1);

            //计算点的位置
            foreach (FlowInfo f in FlowInfos) 
                f.FlowData = 96 - (f.FlowData / est) * 96;
            
        }
    }
}
