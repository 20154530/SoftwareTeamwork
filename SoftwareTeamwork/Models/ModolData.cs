using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareTeamwork {

    public class AccountInfo {
        public string AccountName { get; set; }
        public List<CourseSet> CourseInfos { get; set; }
    }

    public class CourseSet {
        public string Term { get; set; }
        public List<Course> Courses { get; set; }
    }

    public class Course {
        public string CourseName { get; set; }
        public string CourseTeacher { get; set; }
        public string CourseLoc { get; set; }
        public CourseTime CourseTime { get; set; }
        public string CourseDur { get; set; }

        public Course() {
            
        }

        public override string ToString() {
            if (CourseName == null) {
                return String.Format("<Name:{0} Teacher:{1} Loc:{2} Week:{3}  |{4}-{5}| >"
                    , "NULL"
                    , "NULL"
                    , "NULL"
                    , "NULL"
                    , CourseTime.WeekDay
                    , CourseTime.DayTime);
            }
            else {
                return String.Format("<Name:{0} Teacher:{1} Loc:{2} Week:{3}  |{4}-{5}| >"
                    , CourseName.PadLeft(10)
                    , CourseTeacher.PadLeft(5)
                    , CourseLoc.PadLeft(5)
                    , CourseDur
                    , CourseTime.WeekDay
                    , CourseTime.DayTime);
            }
        }
    }

    public class CourseTime {
        public enum DAYS { MON, TUE, WED, THU, FRI, SAT, SUN }
        public enum TERM { C1, C2, C3, C4, C5, C6 }
        public DAYS WeekDay { get; set; }
        public TERM DayTime { get; set; }

        public override string ToString() {
            return String.Format("{0}-{1}", Convert.ToInt32(WeekDay), Convert.ToInt32(DayTime));
        }

        public static CourseTime FromString(string s) {
            CourseTime temp = new CourseTime();
            string[] ts = s.Split('-');
            temp.WeekDay = (CourseTime.DAYS)Convert.ToInt32(ts[0]);
            temp.DayTime = (CourseTime.TERM)Convert.ToInt32(ts[1]);
            return temp;
        }
    }

    public class FluxInfo {
        public double FluxData { get; set; }
        public double Balance { get; set; }
        public DateTime InfoTime { get; set; }

        public FluxInfo() {
            FluxData = 0.0;
            Balance = 0.0; InfoTime = new DateTime(2000, 1, 1, 0, 0, 0);
        }

        public string[] GetXmlItemStyle() {
            string[] pairs = new string[]{
                String.Format("Hour:{0}", InfoTime.Hour),
                String.Format("Min:{0}", InfoTime.Minute),
                String.Format("Sec:{0}", InfoTime.Second),
                String.Format("FluxData:{0}", FluxData),
                String.Format("Balance:{0}", Balance),
            };
            return pairs;
        }
        public string[] GetXmlDateStyle() {
            string[] pairs = new string[]{
                String.Format("Year:{0}", InfoTime.Year),
                String.Format("Mon:{0}", InfoTime.Month),
                String.Format("Day:{0}", InfoTime.Day),
            };
            return pairs;
        }
    }

}