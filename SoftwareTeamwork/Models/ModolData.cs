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

        //移除非本周课程
        public void RemoveNotNow() {
            if (Courses.Count > 0) {
                for (int i = 0; i < Courses.Count; i++) {
                    if (Courses[i].CourseName != "") {
                        if (Courses[i].CourseDur.Length <= 2) {
                            if (Convert.ToInt32(Courses[i].CourseDur) != Properties.Settings.Default.WeekNow) {
                                if (Courses[i].CourseTime.Equals(Courses[i + 1].CourseTime) || (i > 1 && Courses[i].CourseTime.Equals(Courses[i - 1].CourseTime))) {
                                    Courses.RemoveAt(i);
                                    i--;
                                }
                                else {
                                    Courses[i] = new Course() { CourseTime = Courses[i].CourseTime };
                                }
                            }
                        }
                        else if (Courses[i].CourseDur.Length <= 6) {
                            string[] during = Courses[i].CourseDur.Split('-');
                            if (during.Length > 1) {
                                int begin = Convert.ToInt32(during[0]);
                                int end = Convert.ToInt32(during[1]);
                                if (Properties.Settings.Default.WeekNow < begin || Properties.Settings.Default.WeekNow > end) {
                                    if (Courses[i].CourseTime.Equals(Courses[i + 1].CourseTime) || (i > 1 && Courses[i].CourseTime.Equals(Courses[i - 1].CourseTime))) {
                                        Courses.RemoveAt(i);
                                        i--;
                                    }
                                    else {
                                        Courses[i] = new Course() { CourseTime = Courses[i].CourseTime };
                                    }
                                }
                            }
                        }
                        else {
                            string[] during = Courses[i].CourseDur.Split(new char[] { '-', ':' });
                            if (during.Length > 1) {
                                int begin1 = Convert.ToInt32(during[0]);
                                int begin2 = Convert.ToInt32(during[2]);
                                int end1 = Convert.ToInt32(during[1]);
                                int end2 = Convert.ToInt32(during[3]);
                                if ((Properties.Settings.Default.WeekNow < begin1 || Properties.Settings.Default.WeekNow > end1) ||
                                    (Properties.Settings.Default.WeekNow < begin2 || Properties.Settings.Default.WeekNow > end2)) {
                                    if (Courses[i].CourseTime.Equals(Courses[i + 1].CourseTime) || (i > 1 && Courses[i].CourseTime.Equals(Courses[i - 1].CourseTime))) {
                                        Courses.RemoveAt(i);
                                        i--;
                                    }
                                    else {
                                        Courses[i] = new Course() { CourseTime = Courses[i].CourseTime };
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public override string ToString() {
            string s = "";
            s += Term + "\n";
            foreach (Course c in Courses) {
                s += c.ToString()+ "\n";
            }
            return s;
        }
    }

    public class Course {
        public string CourseName { get; set; }
        public string CourseTeacher { get; set; }
        public string CourseLoc { get; set; }
        public CourseTime CourseTime { get; set; }
        public string CourseDur { get; set; }

        public Course() {
            CourseName = "";
            CourseTeacher = "";
            CourseLoc = "";
            CourseDur = "";
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

        public override bool Equals(object obj) {
            CourseTime time = (CourseTime)obj;
            return time.DayTime.Equals(DayTime) && time.WeekDay.Equals(WeekDay);
        }

        public override int GetHashCode() {
            var hashCode = -735683111;
            hashCode = hashCode * -1521134295 + WeekDay.GetHashCode();
            hashCode = hashCode * -1521134295 + DayTime.GetHashCode();
            return hashCode;
        }
    }

    public class FluxInfo {
        public double FluxData { get; set; }
        public double Balance { get; set; }
        public DateTime InfoTime { get; set; }

        public FluxInfo() {
            FluxData = 0.0;
            Balance = 0.0;
            InfoTime = DateTime.Now ;
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