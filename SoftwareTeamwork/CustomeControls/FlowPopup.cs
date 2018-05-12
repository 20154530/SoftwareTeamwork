using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SoftwareTeamwork {
    public class FlowPopup : DPopup {

        #region IconPath
        public String IconPath {
            get { return (String)GetValue(IconPathProperty); }
            set { SetValue(IconPathProperty, value); }
        }
        public static readonly DependencyProperty IconPathProperty =
             DependencyProperty.Register("IconPath", typeof(String), typeof(FlowPopup), new PropertyMetadata(""));
        #endregion

        #region 百分比
        public int Percent {
            get { return (int)GetValue(PercentProperty); }
            set { SetValue(PercentProperty, value); }
        }
        public static readonly DependencyProperty PercentProperty =
            DependencyProperty.Register("Percent", typeof(int), typeof(FlowPopup), new PropertyMetadata(100));
        #endregion

        public void SetIconPathByPercentAngle(double a) {
            var A = a * 2 * Math.PI / 360;
            var x = 20 * Math.Sin(A);
            var y = 20 * Math.Cos(A);
            x = 30 + x;
            y = 30 - y;
            if (a <= 180)
                IconPath = "M 30,10 A 20,20,0,0,1," + x.ToString() + "," + y.ToString();
            else
                IconPath = "M 30,10 A 20,20,0,1,1," + x.ToString() + "," + y.ToString();
        }

        public FlowPopup() {
            OverallSettingManger.Instence.ThemeChanged += Instence_ThemeChanged;
        }

        private void Instence_ThemeChanged(object sender, EventArgs e) {
            this.Style = null;
            this.Style = (Style)Application.Current.FindResource("MainFlowPopup");
        }
    }
}
