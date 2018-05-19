using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Color = System.Windows.Media.Color;

namespace SoftwareTeamwork {
    class CourseTable : Window {

        private string[][] HTitlesTable = new string[3][] {
            new string[]{ "星期一","星期二","星期三","星期四","星期五","星期六","星期日" },
            new string[]{ "MON","TUE","WED","THU","FRI","SAT","SUN"},
            new string[]{ "一","二", "三", "四", "五", "六", "七" }
        };

        private string[][] VTitlesTable = new string[3][] {
            new string[]{ "一","二", "三", "四", "五", "六"},
            new string[]{ "1","2","3","4","5","6"},
            new string[]{  }
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

        #region 
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

        #region PrivateMethods
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

        private void InitTable() {
            HTitles = HTitlesTable[0];
            VTitles = VTitlesTable[1];
        }


        #endregion

        #region overrides
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
            this.DragMove();
            base.OnMouseLeftButtonDown(e);
        }

        public new void ShowDialog() {
            if (base.Visibility.Equals(Visibility.Visible)) {
                return;
            }
            else {
                base.ShowDialog();
            }
        }

        protected override void OnClosing(CancelEventArgs e) {
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
        #endregion


        private void Instence_OnCFontSizeChanged(object sender, EventArgs e) {
            FirstLevelTextSize = (double)sender;
        }

        public CourseTable() {
            CourseSet = DataFormater.Instense.CourseSet;
            if(CourseSet is null) 
                MessageService.Instence.ShowError(null,"没有课程信息，请检查网络，教务处信息");
            OverallSettingManger.Instence.OnCFontSizeChanged += Instence_OnCFontSizeChanged;
            Style = Application.Current.FindResource("CourseTableWidget") as Style;
            CreatLists();
            InitTable();
        }

        static CourseTable() { }
    }
}