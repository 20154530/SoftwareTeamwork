﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using Color = System.Windows.Media.Color;

namespace SoftwareTeamwork {
    class CourseTable : Window {
        private DragBorder SizeBorder;
        private IconButton SettingButton;
        private bool SizeBorderVisibility = false;
        private System.Timers.Timer RefrashDateTimer;

        private string[][] HTitlesTable = new string[3][] {
            new string[]{ "星期一","星期二","星期三","星期四","星期五","星期六","星期日" },
            new string[]{ "MON","TUE","WED","THU","FRI","SAT","SUN"},
            new string[]{ "一","二", "三", "四", "五", "六", "七" }
        };

        private string[][] VTitlesTable = new string[2][] {
            new string[]{ "一","二", "三", "四", "五", "六"},
            new string[]{ "1","2","3","4","5","6"}
        };

        #region CourseSet
        public CourseSet CourseSet {
            get { return (CourseSet)GetValue(CourseSetProperty); }
            set { SetValue(CourseSetProperty, value); }
        }
        public static readonly DependencyProperty CourseSetProperty =
            DependencyProperty.Register("CourseSet", typeof(CourseSet),
                typeof(CourseTable), new PropertyMetadata(new CourseSet()));
        #endregion

        #region Date
        public DateTime Date {
            get { return (DateTime)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }
        public static readonly DependencyProperty DateProperty =
            DependencyProperty.Register("Date", typeof(DateTime),
                typeof(CourseTable), new PropertyMetadata(DateTime.Now));
        #endregion

        #region CourseLists

        #region MONList
        public ObservableCollection<Course> MONList {
            get { return (ObservableCollection<Course>)GetValue(MONListProperty); }
            set { SetValue(MONListProperty, value); }
        }
        public static readonly DependencyProperty MONListProperty =
            DependencyProperty.Register("MONList", typeof(ObservableCollection<Course>),
                typeof(CourseTable), new PropertyMetadata(null));
        #endregion

        #region TUEList
        public ObservableCollection<Course> TUEList {
            get { return (ObservableCollection<Course>)GetValue(TUEListProperty); }
            set { SetValue(TUEListProperty, value); }
        }
        public static readonly DependencyProperty TUEListProperty =
            DependencyProperty.Register("TUEList", typeof(ObservableCollection<Course>),
                typeof(CourseTable), new PropertyMetadata(null));
        #endregion

        #region WEDList
        public ObservableCollection<Course> WEDList {
            get { return (ObservableCollection<Course>)GetValue(WEDListProperty); }
            set { SetValue(WEDListProperty, value); }
        }
        public static readonly DependencyProperty WEDListProperty =
            DependencyProperty.Register("WEDList", typeof(ObservableCollection<Course>),
                typeof(CourseTable), new PropertyMetadata(null));
        #endregion

        #region THUList
        public ObservableCollection<Course> THUList {
            get { return (ObservableCollection<Course>)GetValue(THUListProperty); }
            set { SetValue(THUListProperty, value); }
        }
        public static readonly DependencyProperty THUListProperty =
            DependencyProperty.Register("THUList", typeof(ObservableCollection<Course>),
                typeof(CourseTable), new PropertyMetadata(null));
        #endregion

        #region FRIList
        public ObservableCollection<Course> FRIList {
            get { return (ObservableCollection<Course>)GetValue(FRIListProperty); }
            set { SetValue(FRIListProperty, value); }
        }
        public static readonly DependencyProperty FRIListProperty =
            DependencyProperty.Register("FRIList", typeof(ObservableCollection<Course>),
                typeof(CourseTable), new PropertyMetadata(null));
        #endregion

        #region SATList
        public ObservableCollection<Course> SATList {
            get { return (ObservableCollection<Course>)GetValue(SATListProperty); }
            set { SetValue(SATListProperty, value); }
        }
        public static readonly DependencyProperty SATListProperty =
            DependencyProperty.Register("SATList", typeof(ObservableCollection<Course>),
                typeof(CourseTable), new PropertyMetadata(null));
        #endregion

        #region SUNList
        public ObservableCollection<Course> SUNList {
            get { return (ObservableCollection<Course>)GetValue(SUNListProperty); }
            set { SetValue(SUNListProperty, value); }
        }
        public static readonly DependencyProperty SUNListProperty =
            DependencyProperty.Register("SUNList", typeof(ObservableCollection<Course>),
                typeof(CourseTable), new PropertyMetadata(null));
        #endregion

        #endregion

        //TitleStyle

        #region HTitles
        public string[] HTitles {
            get { return (string[])GetValue(HTitlesProperty); }
            set { SetValue(HTitlesProperty, value); }
        }
        public static readonly DependencyProperty HTitlesProperty =
            DependencyProperty.Register("HTitles", typeof(string[]), 
                typeof(CourseTable), new PropertyMetadata(new string[7]));
        #endregion

        #region VTitles
        public string[] VTitles {
            get { return (string[])GetValue(VTitlesProperty); }
            set { SetValue(VTitlesProperty, value); }
        }
        public static readonly DependencyProperty VTitlesProperty =
            DependencyProperty.Register("VTitles", typeof(string[]),
                typeof(CourseTable), new PropertyMetadata(new string[6]));
        #endregion

        #region CourseTextAlignment
        public TextAlignment CourseTextAlignment {
            get { return (TextAlignment)GetValue(CourseTextAlignmentProperty); }
            set { SetValue(CourseTextAlignmentProperty, value); }
        }
        public static readonly DependencyProperty CourseTextAlignmentProperty =
            DependencyProperty.Register("CourseTextAlignment", typeof(TextAlignment),
                typeof(CourseTable), new PropertyMetadata(TextAlignment.Center));
        #endregion

        //Brushs

        #region CourseTableBorder
        public SolidColorBrush CourseTableBorder {
            get { return (SolidColorBrush)GetValue(CourseTableBorderProperty); }
            set { SetValue(CourseTableBorderProperty, value); }
        }
        public static readonly DependencyProperty CourseTableBorderProperty =
            DependencyProperty.Register("CourseTableBorder", typeof(SolidColorBrush), typeof(CourseTable),
                new PropertyMetadata(new SolidColorBrush(Color.FromArgb(160, 255, 255, 255))));
        #endregion

        #region TextBrush
        public SolidColorBrush TextBrush {
            get { return (SolidColorBrush)GetValue(TextBrushProperty); }
            set { SetValue(TextBrushProperty, value); }
        }
        public static readonly DependencyProperty TextBrushProperty =
            DependencyProperty.Register("TextBrush", typeof(SolidColorBrush), typeof(CourseTable), 
                new PropertyMetadata(new SolidColorBrush(Color.FromArgb(255,255,255,255))));
        #endregion

        #region TitleBackGround
        public SolidColorBrush TitleBackGround {
            get { return (SolidColorBrush)GetValue(TitleBackGroundProperty); }
            set { SetValue(TitleBackGroundProperty, value); }
        }
        public static readonly DependencyProperty TitleBackGroundProperty =
            DependencyProperty.Register("TitleBackGround", typeof(SolidColorBrush), typeof(CourseTable),
                new PropertyMetadata(new SolidColorBrush(Color.FromArgb(160, 255, 255, 255))));
        #endregion

        #region TodayBrush
        public Brush TodayBrush {
            get { return (Brush)GetValue(TodayBrushProperty); }
            set { SetValue(TodayBrushProperty, value); }
        }
        public static readonly DependencyProperty TodayBrushProperty =
            DependencyProperty.Register("TodayBrush", typeof(Brush),
                typeof(CourseTable), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));
        #endregion

        //Sizes

        #region CourseTableBorderThickness
        public double TableBorderThickness {
            get { return (double)GetValue(TableBorderThicknessProperty); }
            set { SetValue(TableBorderThicknessProperty, value); }
        }
        public static readonly DependencyProperty TableBorderThicknessProperty =
            DependencyProperty.Register("TableBorderThickness", typeof(double),
                typeof(CourseTable), new PropertyMetadata(1.0));
        #endregion

        #region FirstLevelTextSize
        public double FirstLevelTextSize {
            get { return (double)GetValue(FirstLevelTextSizeProperty); }
            set { SetValue(FirstLevelTextSizeProperty, value); }
        }
        public static readonly DependencyProperty FirstLevelTextSizeProperty =
            DependencyProperty.Register("FirstLevelTextSize", typeof(double), 
                typeof(CourseTable), new PropertyMetadata(12.0));
        #endregion

        #region SecondLevelTextSize
        public double SecondLevelTextSize {
            get { return (double)GetValue(SecondLevelTextSizeProperty); }
            set { SetValue(SecondLevelTextSizeProperty, value); }
        }
        public static readonly DependencyProperty SecondLevelTextSizeProperty =
            DependencyProperty.Register("SecondLevelTextSize", typeof(double),
                typeof(CourseTable), new PropertyMetadata(10.0));
        #endregion

        //Converters

        #region PrivateMethods
        private void InitTimer() {
            RefrashDateTimer = new System.Timers.Timer();
            RefrashDateTimer.Interval = 3600000;
            RefrashDateTimer.Elapsed += RefrashDate;
        }

        private void CreatLists() {
            MONList = new ObservableCollection<Course>();
            TUEList = new ObservableCollection<Course>();
            WEDList = new ObservableCollection<Course>();
            THUList = new ObservableCollection<Course>();
            FRIList = new ObservableCollection<Course>();
            SATList = new ObservableCollection<Course>();
            SUNList = new ObservableCollection<Course>();
            for (int i = 0; i < CourseSet.Courses.Count; i++) {
                switch (i % 7) {
                    case 0: MONList.Add(CourseSet.Courses[i]); break;
                    case 1: TUEList.Add(CourseSet.Courses[i]); break;
                    case 2: WEDList.Add(CourseSet.Courses[i]); break;
                    case 3: THUList.Add(CourseSet.Courses[i]); break;
                    case 4: FRIList.Add(CourseSet.Courses[i]); break;
                    case 5: SATList.Add(CourseSet.Courses[i]); break;
                    case 6: SUNList.Add(CourseSet.Courses[i]); break;
                }
            }
        }

        private void InitTitle() {
            switch (Properties.Settings.Default.CourseTableTitleStyle) {
                case 0:
                    HTitles = HTitlesTable[0];
                    VTitles = VTitlesTable[0];
                    break;
                case 1:
                    HTitles = HTitlesTable[0];
                    VTitles = VTitlesTable[1];
                    break;
                case 2:
                    HTitles = HTitlesTable[1];
                    VTitles = VTitlesTable[0];
                    break;
                case 3:
                    HTitles = HTitlesTable[1];
                    VTitles = VTitlesTable[1];
                    break;
                case 4:
                    HTitles = HTitlesTable[2];
                    VTitles = VTitlesTable[0];
                    break;
                case 5:
                    HTitles = HTitlesTable[2];
                    VTitles = VTitlesTable[1];
                    break;
            }
        }

        private void InitListeners() {
            OverallSettingManger.Instence.OnCFontSizeChanged += Instence_OnCFontSizeChanged;
            OverallSettingManger.Instence.OnCFontColorChanged += Instence_OnCFontColorChanged;
            OverallSettingManger.Instence.OnCBackgroundColorChanged += Instence_OnCBackgroundColorChanged;
            OverallSettingManger.Instence.OnCTitleColorChanged += Instence_OnCTitleColorChanged;
            OverallSettingManger.Instence.WeekChanged += Instence_WeekChanged;
            OverallSettingManger.Instence.CourseTableTitleStyleChanged += Instence_CourseTableTitleStyleChanged;
            OverallSettingManger.Instence.OnCTodayColorChanged += Instence_OnCTodayColorChanged;
            App.Current.Exit += Current_Exit;
        }

        private void InitColor() {
            TitleBackGround = new SolidColorBrush(DataFormater.GetMediaColor(Properties.Settings.Default.CourseTableTitleBackground));
            Background = new SolidColorBrush(DataFormater.GetMediaColor(Properties.Settings.Default.CourseTableBackground));
            TextBrush = new SolidColorBrush(DataFormater.GetMediaColor(Properties.Settings.Default.CourseTableTextColor));
            FirstLevelTextSize = Properties.Settings.Default.CourseTableFontSize;
            TodayBrush = new SolidColorBrush(DataFormater.GetMediaColor(Properties.Settings.Default.CourseTableTodayBrush));
        }
        #endregion

        #region overrides
        protected override void OnMouseEnter(MouseEventArgs e) {
            SettingButton.Visibility = Visibility.Visible;
            SizeBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(160, 0, 0, 0));
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e) {
            SettingButton.Visibility = Visibility.Hidden;
            if (!SizeBorderVisibility)
                SizeBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e) {
            if (!SizeBorderVisibility) {
                SizeBorderVisibility = true;
                SizeBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(160, 0, 0, 0));
            }
            else {
                SizeBorderVisibility = false;
                SizeBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(160, 0, 0, 0));
            }
            base.OnMouseDoubleClick(e);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
            Win32APIs.ReleaseCapture();
            Win32APIs.SendMessage(new WindowInteropHelper(this).Handle, 0x112, (IntPtr)0xF012, IntPtr.Zero);
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnClosing(CancelEventArgs e) {
            //释放资源
            if (Width > 320 && Height > 240)
                Properties.Settings.Default.CourseTableSize = new System.Drawing.Size((int)Width, (int)Height);
            else
                Properties.Settings.Default.CourseTableSize = new System.Drawing.Size(320, 240);
            HTitles = null;
            VTitles = null;
            MONList = null;
            TUEList = null;
            WEDList = null;
            THUList = null;
            FRIList = null;
            SATList = null;
            SUNList = null;
            GC.Collect();
            base.OnClosing(e);
        }

        public override void OnApplyTemplate() {
            SizeBorder = ((DragBorder)GetTemplateChild("DragSide"));
            SizeBorder. AttachedWindow = this;
            SettingButton = ((IconButton)GetTemplateChild("OpenSetting"));
            SettingButton.Visibility = Visibility.Hidden;
            SettingButton.Click += SettingButton_Click;
            base.OnApplyTemplate();
        }

        private void SettingButton_Click(object sender, RoutedEventArgs e) {
            Application.Current.MainWindow.Show();
            OverallSettingManger.Instence.OpenTrigger = true;
        }
        #endregion

        #region 更新通知

        private void Instence_OnCTodayColorChanged(object sender, EventArgs e) {
            TodayBrush = new SolidColorBrush((Color)sender);
        }

        private void Instence_CourseTableTitleStyleChanged(object sender, EventArgs e) {
            InitTitle();
        }

        private void Instence_OnCFontSizeChanged(object sender, EventArgs e) {
            FirstLevelTextSize = (double)sender;
        }

        private void Instence_OnCTitleColorChanged(object sender, EventArgs e) {
            TitleBackGround = new SolidColorBrush((Color)sender);
        }

        private void Instence_OnCBackgroundColorChanged(object sender, EventArgs e) {
            Background = new SolidColorBrush((Color)sender);
        }

        private void Instence_OnCFontColorChanged(object sender, EventArgs e) {
            TextBrush = new SolidColorBrush((Color)sender);
        }

        private void Instence_WeekChanged(object sender, EventArgs e) {
            CourseSet = DataFormater.Instense.GetCourse();
            CreatLists();
            InitTitle();
        }


        private void Current_Exit(object sender, ExitEventArgs e) {
            this.Close();
        }


        private void RefrashDate(object sender, System.Timers.ElapsedEventArgs e) {
            TodayBrush = new SolidColorBrush((Color)sender);
        }
        #endregion

        public CourseTable() {
            CourseSet = DataFormater.Instense.CourseSet;
            if (CourseSet is null) {
                MessageService.Instence.ShowError(null, "没有课程信息，请检查网络，教务处信息");
                this.Close();
                return;
            }
            Height = Properties.Settings.Default.CourseTableSize.Height;
            Width = Properties.Settings.Default.CourseTableSize.Width;
            Style = Application.Current.FindResource("CourseTableWidget") as Style;
            InitTimer();
            InitListeners();
            InitColor();
            CreatLists();
            InitTitle();
        }

        static CourseTable() { }
    }
}