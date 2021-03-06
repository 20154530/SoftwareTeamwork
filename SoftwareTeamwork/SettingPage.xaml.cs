﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Drawing = System.Drawing;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoftwareTeamwork {
    /// <summary>
    /// SettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingPage : Page {

        #region AreaIconFontSize
        public List<Double> AreaIconFontSize;
        #endregion

        #region TitleStyle
        public List<string> TitleStyle;
        #endregion

        public SettingPage() {
            AreaIconFontSize = new List<double>();
            TitleStyle = new List<string>();
            for (int i = 90; i <= 120; i += 2) {
                AreaIconFontSize.Add(i/10.0);
            }
            TitleStyle.Add("星期三 - 三");
            TitleStyle.Add("星期三 - 3");
            TitleStyle.Add("WED - 三");
            TitleStyle.Add("WED - 3");
            TitleStyle.Add("三 - 三");
            TitleStyle.Add("三 - 3");
            InitializeComponent();
            Loaded += SettingPage_Loaded;
        }

        private void SettingPage_Loaded(object sender, RoutedEventArgs e) {
            AFontSize.ItemsSource = AreaIconFontSize;
            CFontSize.ItemsSource = AreaIconFontSize;
            CTitleStyle.ItemsSource = TitleStyle;
            InitControls();
        }

        private void InitControls() {
            ShowExit.IsChecked = Properties.Settings.Default.IsExitDirectly;
            ExitAction.IsChecked = Properties.Settings.Default.ExitAction;
        }

        private void Reset(object sender, RoutedEventArgs e) {
            QuestionDialog dialog = new QuestionDialog {
                Style = (Style)Application.Current.FindResource("QuestionDialog"),
                Context = "确定还原默认设置,包括用户数据?"
            };
            dialog.ShowDialog(Application.Current.MainWindow);

            if (dialog.DialogResult.Equals(true)) {
                OverallSettingManger.Instence.Reset();
            }
        }

        private void OtherSetting(object sender, RoutedEventArgs e) {
            switch (((Control)sender).Name) {
                case "ShowExit":
                    Properties.Settings.Default.IsExitDirectly = (bool)ShowExit.IsChecked;
                    break;
                case "ExitAction":
                    Properties.Settings.Default.ExitAction = (bool)ExitAction.IsChecked;
                    break;
            }
        }

        private void ChangeFontSize(object sender, SelectionChangedEventArgs e) {
            switch (((Control)sender).Name) {
                case "AFontSize":
                    OverallSettingManger.Instence.AFontSize = (double)AFontSize.SelectedValue;
                    break;
                case "CFontSize":
                    OverallSettingManger.Instence.CFontSize = (double)CFontSize.SelectedValue;
                    break;
            }
        }

        private void Color(object sender, RoutedEventArgs e) {
            ColorPicker picker = new ColorPicker();
            picker.Style = Application.Current.FindResource("DefaultColorPicker") as Style;
            Color co;
            Drawing.Color dco;
            switch (((Control)sender).Name) {
                case "AColor":
                    picker.ShowDialogD(App.Current.MainWindow, OverallSettingManger.Instence.AFontColor);
                    dco = picker.DrawingGetColor();
                    if ((bool)picker.DialogResult)
                        OverallSettingManger.Instence.AFontColor = dco;
                    break;
                case "CBGColor":
                    picker.ShowDialog(App.Current.MainWindow, OverallSettingManger.Instence.CBackgroundColor);
                    co = picker.GetColor();
                    if ((bool)picker.DialogResult)
                        OverallSettingManger.Instence.CBackgroundColor = co;
                    break;
                case "CTColor":
                    picker.ShowDialog(App.Current.MainWindow, OverallSettingManger.Instence.CTitleColor);
                    co = picker.GetColor();
                    if ((bool)picker.DialogResult)
                        OverallSettingManger.Instence.CTitleColor = co;
                    break;
                case "CFColor":
                    picker.ShowDialog(App.Current.MainWindow, OverallSettingManger.Instence.CFontColor);
                    co = picker.GetColor();
                    if ((bool)picker.DialogResult)
                        OverallSettingManger.Instence.CFontColor = co;
                    break;
                case "TBGColor":
                    picker.ShowDialog(App.Current.MainWindow, OverallSettingManger.Instence.CTodayColor);
                    co = picker.GetColor();
                    if ((bool)picker.DialogResult)
                        OverallSettingManger.Instence.CTodayColor = co;
                    break;
            }
        }

        private void WeekSetting(object sender, KeyEventArgs e) {
            if (e.Key.Equals(Key.Enter)) {
                int week = Convert.ToInt32(((TextBox)sender).Text);
                if (week < 0 && week > 22) {
                    MessageService.Instence.ShowError(App.Current.MainWindow, "输入范围错误，请重新输入");
                    return;
                }
                Properties.Settings.Default.WeekNow = week;
                int dayoffset = Convert.ToInt32( DateTime.Now.DayOfWeek);
                if(dayoffset == 0)
                    OverallSettingManger.Instence.WeekNowSet = DateTime.Now.AddDays(-7);
                OverallSettingManger.Instence.WeekNowSet = DateTime.Now.AddDays(-dayoffset);
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e) {
            int dayoffset = Convert.ToInt32(DateTime.Now.DayOfWeek);
            if (dayoffset == 0)
                OverallSettingManger.Instence.WeekNowSet = DateTime.Now.AddDays(-7);
            OverallSettingManger.Instence.WeekNowSet = DateTime.Now.AddDays(-dayoffset);
        }

        private void ChangeTitle(object sender, SelectionChangedEventArgs e) {
            switch (CTitleStyle.SelectedValue) {
                case "星期三 - 三":
                    OverallSettingManger.Instence.CourseTableTitleStyle = 0;
                    break;
                case "星期三 - 3":
                    OverallSettingManger.Instence.CourseTableTitleStyle = 1;
                    break;
                case "WED - 三":
                    OverallSettingManger.Instence.CourseTableTitleStyle = 2;
                    break;
                case "WED - 3":
                    OverallSettingManger.Instence.CourseTableTitleStyle = 3;
                    break;
                case "三 - 三":
                    OverallSettingManger.Instence.CourseTableTitleStyle = 4;
                    break;
                default:
                    OverallSettingManger.Instence.CourseTableTitleStyle = 5;
                    break;
            }
        }

        private void IsSelfRunning(object sender, RoutedEventArgs e) {
            Properties.Settings.Default.IsRunAtStart = (bool)SelfRuning.IsChecked;
            OverallSettingManger.SetSelfRunning(Properties.Settings.Default.IsRunAtStart);
        }
    }
}
