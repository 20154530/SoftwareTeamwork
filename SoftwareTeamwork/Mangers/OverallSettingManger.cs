using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Drawing = System.Drawing;
using Color = System.Windows.Media.Color;

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

        #region AFontSize
        public event EventHandler OnAFontSizeChanged;
        private double aFontSize = Properties.Settings.Default.AreaIconFontSize;
        public double AFontSize {
            get => aFontSize;
            set {
                OnAFontSizeChanged?.Invoke(value, null);
                aFontSize = value;
                Properties.Settings.Default.AreaIconFontSize = value;
            }
        }
        #endregion

        #region AFontColor
        public event EventHandler OnAFontColorChanged;
        private Drawing.Color aFontColor = Properties.Settings.Default.AreaIconColor;
        public Drawing.Color AFontColor {
            get => aFontColor;
            set {
                OnAFontColorChanged?.Invoke(value, null);
                aFontColor = value;
                Properties.Settings.Default.AreaIconColor = value;
            }
        }
        #endregion

        #region CFontSize
        public event EventHandler OnCFontSizeChanged;
        private double cFontSize = Properties.Settings.Default.CourseTableFontSize;
        public double CFontSize {
            get => cFontSize;
            set {
                OnCFontSizeChanged?.Invoke(value, null);
                cFontSize = value;
                Properties.Settings.Default.CourseTableFontSize = value;
            }
        }
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
