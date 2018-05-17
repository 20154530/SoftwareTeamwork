using System;
using System.Collections.Generic;
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

        #region
        #endregion

        public CourseTable() {
            CourseSet = DataFormater.Instense.GetCourse();
            CourseSet.RemoveNotNow();
            Console.WriteLine(CourseSet);
        }
    }
}