using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SoftwareTeamwork {
    class CourseTable : DPopup {

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

        #region MONList
        public ObservableCollection<Course> MONList {
            get { return (ObservableCollection<Course>)GetValue(MONListProperty); }
            set { SetValue(MONListProperty, value); }
        }
        public static readonly DependencyProperty MONListProperty =
            DependencyProperty.Register("MONList", typeof(ObservableCollection<Course>),
                typeof(CourseTable), new PropertyMetadata(new ObservableCollection<Course>()));
        #endregion

        #region TUEList
        public ObservableCollection<Course> TUEList {
            get { return (ObservableCollection<Course>)GetValue(TUEListProperty); }
            set { SetValue(TUEListProperty, value); }
        }
        public static readonly DependencyProperty TUEListProperty =
            DependencyProperty.Register("TUEList", typeof(ObservableCollection<Course>),
                typeof(CourseTable), new PropertyMetadata(new ObservableCollection<Course>()));
        #endregion

        #region WEDList
        public ObservableCollection<Course> WEDList {
            get { return (ObservableCollection<Course>)GetValue(WEDListProperty); }
            set { SetValue(WEDListProperty, value); }
        }
        public static readonly DependencyProperty WEDListProperty =
            DependencyProperty.Register("WEDList", typeof(ObservableCollection<Course>),
                typeof(CourseTable), new PropertyMetadata(new ObservableCollection<Course>()));
        #endregion

        #region THUList
        public ObservableCollection<Course> THUList {
            get { return (ObservableCollection<Course>)GetValue(THUListProperty); }
            set { SetValue(THUListProperty, value); }
        }
        public static readonly DependencyProperty THUListProperty =
            DependencyProperty.Register("THUList", typeof(ObservableCollection<Course>),
                typeof(CourseTable), new PropertyMetadata(new ObservableCollection<Course>()));
        #endregion

        #region FRIList
        public ObservableCollection<Course> FRIList {
            get { return (ObservableCollection<Course>)GetValue(FRIListProperty); }
            set { SetValue(FRIListProperty, value); }
        }
        public static readonly DependencyProperty FRIListProperty =
            DependencyProperty.Register("FRIList", typeof(ObservableCollection<Course>),
                typeof(CourseTable), new PropertyMetadata(new ObservableCollection<Course>()));
        #endregion

        #region SATList
        public ObservableCollection<Course> SATList {
            get { return (ObservableCollection<Course>)GetValue(SATListProperty); }
            set { SetValue(SATListProperty, value); }
        }
        public static readonly DependencyProperty SATListProperty =
            DependencyProperty.Register("SATList", typeof(ObservableCollection<Course>),
                typeof(CourseTable), new PropertyMetadata(new ObservableCollection<Course>()));
        #endregion

        #region SUNList
        public ObservableCollection<Course> SUNList {
            get { return (ObservableCollection<Course>)GetValue(SUNListProperty); }
            set { SetValue(SUNListProperty, value); }
        }
        public static readonly DependencyProperty SUNListProperty =
            DependencyProperty.Register("SUNList", typeof(ObservableCollection<Course>),
                typeof(CourseTable), new PropertyMetadata(new ObservableCollection<Course>()));
        #endregion

        private void CreatLists() {
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

        public CourseTable() {
            CourseSet = DataFormater.Instense.GetCourse();
            CreatLists();
        }
    }
}