using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SoftwareTeamwork {

    class OverallSettingManger {
        public static OverallSettingManger Instence = new OverallSettingManger();

        public enum CloseMode {
            Exit,
            AreaIcon
        }

        #region 当前主题
        public event EventHandler ThemeChanged;
        private String theme = "DefaultTheme.xaml";
        public String Theme {
            get => theme;
            set { theme = value;
                ThemeChanged?.Invoke(this,EventArgs.Empty);
            }
        }
        #endregion

        #region 流量更新
        public event EventHandler OnFluxUpdate;
        private double fluxPercent = 0.0;
        public double FluxPercent {
            get => fluxPercent;
            set { OnFluxUpdate?.Invoke(value, null); fluxPercent = value; }
        }
        #endregion

        #region 

        #endregion

        public async void Reset() {
            Properties.Settings.Default.Reset();
            Task t =  new Task(() => {
                XmlHelper.ResetAll();
            });
            t.Start();
            await t;
            MessageService.Instence.ShowError(App.Current.MainWindow,"已恢复初始化设置");
        }

        public OverallSettingManger()
        {
            FontFamily a = new FontFamily();
        }
    }
}
