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
    public class FlowTrendPopups : DPopup {

        #region MousePoint.X
        public double PosX {
            get { return (double)GetValue(PosXProperty); }
            set { SetValue(PosXProperty, value); }
        }
        public static readonly DependencyProperty PosXProperty =
            DependencyProperty.Register("PosX", typeof(double), typeof(FlowTrendPopups),
                new PropertyMetadata(0.0));
        #endregion

        #region MousePoint.Y
        public double PosY {
            get { return (double)GetValue(PosYProperty); }
            set { SetValue(PosYProperty, value); }
        }
        public static readonly DependencyProperty PosYProperty =
            DependencyProperty.Register("PosY", typeof(double), typeof(FlowTrendPopups),
                new PropertyMetadata(0.0));
        #endregion

        #region DataGroup
        public FlowTradeGroup DataGroup {
            get { return (FlowTradeGroup)GetValue(DataGroupProperty); }
            set { SetValue(DataGroupProperty, value); }
        }
        public static readonly DependencyProperty DataGroupProperty =
            DependencyProperty.Register("DataGroup", typeof(FlowTradeGroup), typeof(FlowTrendPopups),
                new PropertyMetadata(new FlowTradeGroup() {
                    Flow1 = new FlowInfo { FlowData = 10, InfoTime = new DataTime(10, 20) },
                    Flow2 = new FlowInfo { FlowData = 10, InfoTime = new DataTime(10, 20) },
                    Flow3 = new FlowInfo { FlowData = 10, InfoTime = new DataTime(10, 20) },
                    Flow4 = new FlowInfo { FlowData = 10, InfoTime = new DataTime(10, 20) },
                    Flow5 = new FlowInfo { FlowData = 10, InfoTime = new DataTime(10, 20) },
                    Flow6 = new FlowInfo { FlowData = 10, InfoTime = new DataTime(10, 20) },
                    Flow7 = new FlowInfo { FlowData = 10, InfoTime = new DataTime(10, 20) },
                }));
        #endregion

        #region ActrulData
        public FlowTradeGroup ActDataGroup {
            get { return (FlowTradeGroup)GetValue(ActDataGroupProperty); }
            set { SetValue(ActDataGroupProperty, value); }
        }
        public static readonly DependencyProperty ActDataGroupProperty =
            DependencyProperty.Register("ActDataGroup", typeof(FlowTradeGroup), typeof(FlowTrendPopups),
                new PropertyMetadata(new FlowTradeGroup() {
                    Flow1 = new FlowInfo { FlowData = 10, InfoTime = new DataTime(10, 20) },
                    Flow2 = new FlowInfo { FlowData = 10, InfoTime = new DataTime(10, 20) },
                    Flow3 = new FlowInfo { FlowData = 10, InfoTime = new DataTime(10, 20) },
                    Flow4 = new FlowInfo { FlowData = 10, InfoTime = new DataTime(10, 20) },
                    Flow5 = new FlowInfo { FlowData = 10, InfoTime = new DataTime(10, 20) },
                    Flow6 = new FlowInfo { FlowData = 10, InfoTime = new DataTime(10, 20) },
                    Flow7 = new FlowInfo { FlowData = 10, InfoTime = new DataTime(10, 20) },
                }));
        #endregion

        #region CursorVis
        public Visibility CursorVis {
            get { return (Visibility)GetValue(CursorVisProperty); }
            set { SetValue(CursorVisProperty, value); }
        }
        public static readonly DependencyProperty CursorVisProperty =
            DependencyProperty.Register("CursorVis", typeof(Visibility), typeof(FlowTrendPopups),
                new PropertyMetadata(Visibility.Collapsed));
        #endregion

        #region MaxiumFlow
        public int MaxiumFlow {
            get { return (int)GetValue(MaxiumFlowProperty); }
            set { SetValue(MaxiumFlowProperty, value); }
        }
        public static readonly DependencyProperty MaxiumFlowProperty =
            DependencyProperty.Register("MaxiumFlow", typeof(int), typeof(FlowTrendPopups), new PropertyMetadata(0));
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

        public FlowTrendPopups() {
            // Cursor = new Cursor(new MemoryStream(Properties.Resources.Arrow));
        }

    }

    public class FlowTradeGroup {
        public FlowInfo Flow1 { get; set; }
        public FlowInfo Flow2 { get; set; }
        public FlowInfo Flow3 { get; set; }
        public FlowInfo Flow4 { get; set; }
        public FlowInfo Flow5 { get; set; }
        public FlowInfo Flow6 { get; set; }
        public FlowInfo Flow7 { get; set; }

        public void GetFlowData() {

        }

        private void TransformToNode() {

        }
    }
}
