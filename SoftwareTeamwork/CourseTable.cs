using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SoftwareTeamwork {
    class CourseTable : DPopup {

        #region CourseSet
        public CourseSet courseSet {
            get { return (CourseSet)GetValue(courseSetProperty); }
            set { SetValue(courseSetProperty, value); }
        }
        public static readonly DependencyProperty courseSetProperty =
            DependencyProperty.Register("courseSet", typeof(CourseSet),
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


    }
}