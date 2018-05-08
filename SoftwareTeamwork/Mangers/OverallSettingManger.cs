using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SoftwareTeamwork {
    class OverallSettingManger {

        public static OverallSettingManger Instence = new OverallSettingManger();

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

        public OverallSettingManger()
        {
            FontFamily a = new FontFamily();
        }
    }
}
