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
        public int CourseBegin { get; set; }
        public int CourseEnd { get; set; }
    }

    public class CourseTime {
        public enum DAYS { MON, TUE, WED, THU, FRI, SAT, SUN }
        public enum TERM { C1, C2, C3, C4, C5, C6 }
        public DAYS WeekDay { get; set; }
        public TERM DayTime { get; set; }
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