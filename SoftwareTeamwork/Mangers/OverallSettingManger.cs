using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SoftwareTeamwork {
    [Serializable]
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

        #region Closemode
        private CloseMode closemode;
        public CloseMode Closemode {
            get => closemode;
            set { closemode = value; }
        }
        #endregion

        #region RememberCloseMode
        private bool rememberCloseMode = false;
        public bool RememberCloseMode {
            get => rememberCloseMode;
            set {
                rememberCloseMode = value;
            }
        }
        #endregion

        public OverallSettingManger()
        {
            FontFamily a = new FontFamily();
        }
    }
}
