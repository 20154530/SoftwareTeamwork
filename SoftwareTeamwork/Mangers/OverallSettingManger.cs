using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Drawing = System.Drawing;
using Color = System.Windows.Media.Color;
using System.Windows;
using Microsoft.Win32;
using System.Diagnostics;

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
            set {
                theme = value;
                ThemeChanged?.Invoke(this, EventArgs.Empty);
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

        #region CTitleColor
        public event EventHandler OnCTitleColorChanged;
        private Color cTitleColor = DataFormater.GetMediaColor(Properties.Settings.Default.CourseTableTitleBackground);
        public Color CTitleColor {
            get => cTitleColor;
            set {
                OnCTitleColorChanged?.Invoke(value, null);
                cTitleColor = value;
                Properties.Settings.Default.CourseTableTitleBackground = DataFormater.GetDrawingColor(value);
            }
        }
        #endregion

        #region CBackgroundColor
        public event EventHandler OnCBackgroundColorChanged;
        private Color cBackgroundColor = DataFormater.GetMediaColor(Properties.Settings.Default.CourseTableBackground);
        public Color CBackgroundColor {
            get => cBackgroundColor;
            set {
                OnCBackgroundColorChanged?.Invoke(value, null);
                cBackgroundColor = value;
                Properties.Settings.Default.CourseTableBackground = DataFormater.GetDrawingColor(value);
            }
        }
        #endregion

        #region CTodayColor
        public event EventHandler OnCTodayColorChanged;
        private Color cTodayColor = DataFormater.GetMediaColor(Properties.Settings.Default.CourseTableTodayBrush);
        public Color CTodayColor {
            get => cTodayColor;
            set {
                OnCTodayColorChanged?.Invoke(value, null);
                cTodayColor = value;
                Properties.Settings.Default.CourseTableTodayBrush = DataFormater.GetDrawingColor(value);
            }
        }
        #endregion

        #region CFontColor
        public event EventHandler OnCFontColorChanged;
        private Color cFontColor = DataFormater.GetMediaColor(Properties.Settings.Default.CourseTableTextColor);
        public Color CFontColor {
            get => cFontColor;
            set {
                OnCFontColorChanged?.Invoke(value, null);
                cFontColor = value;
                Properties.Settings.Default.CourseTableTextColor = DataFormater.GetDrawingColor(value);
            }
        }
        #endregion

        #region WeekChanged
        public event EventHandler WeekChanged;
        private DateTime weekNowSet = Properties.Settings.Default.WeekNowSet;
        public DateTime WeekNowSet {
            get => weekNowSet;
            set {
                weekNowSet = value;
                Properties.Settings.Default.WeekNowSet = value;
                WeekChanged?.Invoke(value, null);
            }
        }
        #endregion

        #region CourseTableTitleStyle
        public event EventHandler CourseTableTitleStyleChanged;
        private int courseTableTitleStyle = Properties.Settings.Default.CourseTableTitleStyle;
        public int CourseTableTitleStyle {
            get => courseTableTitleStyle;
            set {
                courseTableTitleStyle = value;
                Properties.Settings.Default.CourseTableTitleStyle = value;
                CourseTableTitleStyleChanged?.Invoke(value, null);
            }
        }
        #endregion

        #region OpenSettingPage
        public event EventHandler OpenSettingPage;
        private bool openTrigger;
        public bool OpenTrigger {
            get => openTrigger;
            set {
                OpenSettingPage?.Invoke(null, null);

            }
        }
        #endregion

        public async void Reset() {
            Task t = new Task(() => {
                Properties.Settings.Default.Reset();
                XmlHelper.ResetAll();
                SetSelfRunning(false);
            });
            t.Start();
            await t;
            MessageService.Instence.ShowError(App.Current.MainWindow, "已恢复初始化设置");
        }

        public static void WeekNowCheck() {
            DateTime now = DateTime.Now;
            int dayoffset = now.DayOfYear - Properties.Settings.Default.WeekNowSet.DayOfYear;
            if (dayoffset > 7 || dayoffset < -358) {
                Properties.Settings.Default.WeekNow += 1;
                int daof = Convert.ToInt32(now.DayOfWeek);
                if (daof == 0)
                    OverallSettingManger.Instence.WeekNowSet = DateTime.Now.AddDays(-7);
                OverallSettingManger.Instence.WeekNowSet = DateTime.Now.AddDays(-dayoffset);
            }
        }

        private static bool IsExistKey(string keyName) {
            bool _exist = false;
            try {
                RegistryKey localKey;
                if (Environment.Is64BitOperatingSystem)
                    localKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                else
                    localKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
                RegistryKey runs = localKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (runs == null) {
                    RegistryKey key2 = localKey.CreateSubKey("SOFTWARE");
                    RegistryKey key3 = key2.CreateSubKey("Microsoft");
                    RegistryKey key4 = key3.CreateSubKey("Windows");
                    RegistryKey key5 = key4.CreateSubKey("CurrentVersion");
                    RegistryKey key6 = key5.CreateSubKey("Run");
                    runs = key6;
                }
                string[] runsName = runs.GetValueNames();
                foreach (string strName in runsName) {
                    if (strName.ToUpper() == keyName.ToUpper()) {
                        _exist = true;
                        return _exist;
                    }
                }
            }
            catch { }
            return _exist;

        }

        private static bool SelfRunning(bool isStart, string exeName, string path) {
            try {
                RegistryKey localKey;
                if (Environment.Is64BitOperatingSystem)
                    localKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                else
                    localKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
                RegistryKey key = localKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (key == null) {
                    localKey.CreateSubKey("SOFTWARE//Microsoft//Windows//CurrentVersion//Run");
                }
                if (isStart)//若开机自启动则添加键值对
                {
                    key.SetValue(exeName, path);
                    key.Close();
                }
                else//否则删除键值对
                {
                    string[] keyNames = key.GetValueNames();
                    foreach (string keyName in keyNames) {
                        if (keyName.ToUpper() == exeName.ToUpper()) {
                            key.DeleteValue(exeName);
                            key.Close();
                        }
                    }
                }
            }
            catch (Exception) { }

            return true;
        }

        public static void SetSelfRunning(bool isStart) {
            if (!IsExistKey("SoftwareTeamwork") && isStart) {
                SelfRunning(isStart, "SoftwareTeamwork", Process.GetCurrentProcess().MainModule.FileName + " -s");
            }
            else if (IsExistKey("SoftwareTeamwork") && !isStart) {
                SelfRunning(isStart, "SoftwareTeamwork", Process.GetCurrentProcess().MainModule.FileName + " -s");
            }
        }

        public OverallSettingManger() {
            FontFamily a = new FontFamily();
        }
    }
}