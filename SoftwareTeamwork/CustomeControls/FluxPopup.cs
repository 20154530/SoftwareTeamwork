using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SoftwareTeamwork {
    public class FluxPopup : DPopup {
        private FluxTrendPopup Pop;

        #region IconPath
        public String IconPath {
            get { return (String)GetValue(IconPathProperty); }
            set { SetValue(IconPathProperty, value); }
        }
        public static readonly DependencyProperty IconPathProperty =
             DependencyProperty.Register("IconPath", typeof(String), typeof(FluxPopup), new PropertyMetadata(""));
        #endregion

        #region 百分比
        public int Percent {
            get { return (int)GetValue(PercentProperty); }
            set { SetValue(PercentProperty, value); }
        }
        public static readonly DependencyProperty PercentProperty =
            DependencyProperty.Register("Percent", typeof(int), typeof(FluxPopup), new PropertyMetadata(100));
        #endregion

        #region FluxUsed
        public double FluxUsed {
            get { return (double)GetValue(FluxUsedProperty); }
            set { SetValue(FluxUsedProperty, value); }
        }
        public static readonly DependencyProperty FluxUsedProperty =
            DependencyProperty.Register("FluxUsed", typeof(double), 
                typeof(FluxPopup), new PropertyMetadata(0.0));
        #endregion

        #region FluxRemain
        public double FluxRemain {
            get { return (double)GetValue(FluxRemainProperty); }
            set { SetValue(FluxRemainProperty, value); }
        }
        public static readonly DependencyProperty FluxRemainProperty =
            DependencyProperty.Register("FluxRemain", typeof(double),
                typeof(FluxPopup), new PropertyMetadata(0.0));
        #endregion

        #region OpenTrendCommand
        public WindowCommand OpenTrendCommand {
            get { return (WindowCommand)GetValue(OpenTrendCommandProperty); }
            set { SetValue(OpenTrendCommandProperty, value); }
        }
        public static readonly DependencyProperty OpenTrendCommandProperty =
            DependencyProperty.Register("OpenTrendCommand", typeof(WindowCommand),
                typeof(FluxPopup), new PropertyMetadata(new WindowCommand()));
        #endregion

        #region FrashCommand
        public WindowCommand FrashCommand {
            get { return (WindowCommand)GetValue(FrashCommandProperty); }
            set { SetValue(FrashCommandProperty, value); }
        }
        public static readonly DependencyProperty FrashCommandProperty =
            DependencyProperty.Register("FrashCommand", typeof(WindowCommand),
                typeof(FluxPopup), new PropertyMetadata(new WindowCommand()));
        #endregion

        #region ConnectCommand
        public WindowCommand ConnectCommand {
            get { return (WindowCommand)GetValue(ConnectCommandProperty); }
            set { SetValue(ConnectCommandProperty, value); }
        }
        public static readonly DependencyProperty ConnectCommandProperty =
            DependencyProperty.Register("ConnectCommand", typeof(WindowCommand), 
                typeof(FluxPopup), new PropertyMetadata(new WindowCommand()));
        #endregion

        #region DisConnect
        public WindowCommand DisConnect {
            get { return (WindowCommand)GetValue(DisConnectProperty); }
            set { SetValue(DisConnectProperty, value); }
        }
        public static readonly DependencyProperty DisConnectProperty =
            DependencyProperty.Register("DisConnect", typeof(WindowCommand),
                typeof(FluxPopup), new PropertyMetadata(new WindowCommand()));
        #endregion

        protected override void OnOpened(EventArgs e) {
            SetIconPathByPercentAngle(DataAnalysis.GetFluxPercent(false) * 100);
            FluxUsed = DataAnalysis.GetFluxData(true);
            FluxRemain = DataAnalysis.GetFluxData(false);
        }

        public void SetIconPathByPercentAngle(double a) {
            Percent = (int)a;
            var A = a  * Math.PI * 3.6 / 180;
            var x = 20 * Math.Sin(A);
            var y = 20 * Math.Cos(A);
            x = 30 + x;
            y = 30 - y;
            if (a <= 50)
                IconPath = "M 30,10 A 20,20,0,0,1," + x.ToString() + "," + y.ToString();
            else
                IconPath = "M 30,10 A 20,20,0,1,1," + x.ToString() + "," + y.ToString();
        }

        private void Instence_ThemeChanged(object sender, EventArgs e) {
            this.Style = null;
            this.Style = (Style)Application.Current.FindResource("MainFluxPopup");
        }

        private void OpenTrendCommand_CAction(object para) {//打开流量走势面板委托
            Pop.PlacementRectangle = new Rect(SystemParameters.WorkArea.Width - 5,
                SystemParameters.WorkArea.Height - this.Child.RenderSize.Height - 10, 0, 0);
            Pop.ShowPopupAni();
        }

        private void FrashCommand_CAction(object para) {//刷新按钮命令委托
            double percent = DataAnalysis.GetFluxPercent(true) * 100;
            OverallSettingManger.Instence.FluxPercent = percent;
            SetIconPathByPercentAngle(percent);
            FluxUsed = DataAnalysis.GetFluxData(true);
            FluxRemain = DataAnalysis.GetFluxData(false);
        }

        private void ConnectCommand_CAction(object para) {
            LoginAgent.Instence.Post("NEUIpgw");
        }

        private void DisConnect_CAction(object para) {
            LoginAgent.Instence.Post("NEUIpgw", new Dictionary<string, string>() {
                {"action","logout" }
            });
        }

        private void Pop_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e) {
            Pop.HidePopupAni();
        }

        private void Pop_MouseMove(object sender, System.Windows.Input.MouseEventArgs e) {
            
        }

        public FluxPopup() {
            Pop = new FluxTrendPopup { Style = (Style)Application.Current.FindResource("FluxAnaPanel") };
            Pop.MouseMove += Pop_MouseMove;
            Pop.MouseLeave += Pop_MouseLeave;
            OverallSettingManger.Instence.ThemeChanged += Instence_ThemeChanged;
            OpenTrendCommand.CAction += OpenTrendCommand_CAction;
            FrashCommand.CAction += FrashCommand_CAction;
            ConnectCommand.CAction += ConnectCommand_CAction;
            DisConnect.CAction += DisConnect_CAction;
        }
    }
}
