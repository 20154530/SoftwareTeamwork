using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Text;
using HtmlAgilityPack;
using System.Diagnostics;
using System.Windows.Threading;

namespace SoftwareTeamwork {
    class DataFormater //数据格式化类
    {
        public bool IPGWConnected { get; set; }
        private FluxInfo ipgwInfo = null;
        public FluxInfo IpgwInfo {
            get {
                return ipgwInfo is null ? 
                    ipgwInfo = GetIpgwDataInf(LoginAgent.Instence.GetData("NEUIpgw")) :ipgwInfo;
            }
            set { ipgwInfo = value; }
        }
        private CourseSet courseSet = null;
        public CourseSet CourseSet {
            get {
                return courseSet is null ? //为空则寻找最近的课程列表
                    courseSet = GetCourse() is null ?//最近课程列表为空则寻找网络
                       UpdateCourse() :
                       GetCourse() : 
                       courseSet;
            }
            set { courseSet = value; }
        }
        private HtmlDocument CourseInfo;

        public static DataFormater Instense = new DataFormater();

        public DataFormater() {  }

        #region Ipgw信息格式

        public void UpdateFlux() {
            LoginAgent.Instence.SetInfset(XmlHelper.GetInfWithName("NEUIpgw"));
            ipgwInfo = GetIpgwDataInf(LoginAgent.Instence.GetData(null));
        }

        public double GetFlux() { return IpgwInfo.FluxData; }

        public DateTime GetTime() { return IpgwInfo.InfoTime; }

        public double GetBalance() { return IpgwInfo.Balance; }

        private FluxInfo GetIpgwDataInf(string data)  //Ipgw网关信息格式化获取
        {
            FluxInfo info = new FluxInfo();
            try {
                string[] t = data.Split(new char[] { ',' });
                info.FluxData = FluxFormater(t[0]);
                info.Balance = Convert.ToDouble(t[2]);
                info.InfoTime = DateTime.Now;
            }
            catch (IndexOutOfRangeException) {
                MessageService.Instence.ShowError(App.Current.MainWindow, "用户名或密码错误");
                IPGWConnected = false;
                return null;
            }
            return info;
        }

        private double FluxFormater(string flux)//流量信息格式化 in(Byte)
        {
            Int64 a = 0;
            try {
                a = Convert.ToInt64(flux);
            }
            catch (FormatException) {
                MessageService.Instence.ShowError(null, "数据格式错误，请检查用户名和密码");
            }
            return a / 1000000.0;
        }
        #endregion


        #region 教务处
        private void LoadClassPage() {
            CourseInfo = new HtmlDocument();
            //try { CourseInfo.LoadHtml(LoginAgent.Instence.GetData("NEUZhjw")); }
            //catch (Exception) {
            //    App.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => {
            //        MessageService.Instence.ShowError(App.Current.MainWindow, "请检查网络连接,用户信息设置");
            //    }));
            //}
            CourseInfo.Load("HTMLPage1.html");
        }

        private CourseSet UpdateCourse() {
            LoadClassPage();
            CourseSet cs = new CourseSet {
                Courses = new List<Course>()
            };
            HtmlNodeCollection nodes = CourseInfo.DocumentNode.ChildNodes;
            HtmlNode content = null;
            try {
                content = nodes[3].ChildNodes[3].ChildNodes[1].ChildNodes[3].
                    ChildNodes[1].ChildNodes[1].ChildNodes[3];
            }
            catch (Exception) {
                App.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => {
                    MessageService.Instence.ShowError(App.Current.MainWindow, "登录过期，请重新登录");
                }));
                return null;
            }
            string[] s = content.InnerHtml.Replace("</td>", "S").Split(new string[]
            {   "<br style=\"mso-data-placement:same-cell\">","</tr>",
                "<td valign=\"top\" style=\"font-size:10px;border-top:none;border-right:.5pt solid #B4E2A4;border-bottom:.5pt solid #B4E2A4;border-left:none;white-space:normal\" bgcolor=\"#EBFFE1\">",
                "<td valign=\"top\" style=\"font-size:10px;border-top:none;border-right:.5pt solid #B4E2A4;border-bottom:.5pt solid #B4E2A4;border-left:none;white-space:normal\" bgcolor=\"#FFFFFF\">"
            }, StringSplitOptions.RemoveEmptyEntries);
            int term = 0, place = 0;
            Course cou = new Course();
            foreach (string ss in s) {
                if (ss.Contains("<") || ss.Contains("/") || (ss.Contains(" ") && (!ss.Contains("周") && !ss.Contains("&"))) || ss.Contains("星")) {
                    continue;
                }
                else {
                    var temp = ss.Trim();
                    if (temp.Contains("&nbsp;")) {
                        cs.Courses.Add(new Course() {
                            CourseTime = new CourseTime {
                                WeekDay = (CourseTime.DAYS)(place % 7),
                                DayTime = (CourseTime.TERM)(place / 7),
                            }
                        });
                        place++;
                    }
                    else {
                        switch (term) {
                            case 0:
                                cou = new Course();
                                cou.CourseName = temp;
                                break;
                            case 1:
                                cou.CourseTeacher = temp;
                                break;
                            case 2:
                                cou.CourseLoc = temp;
                                break;
                            case 3:
                                cou.CourseTime = new CourseTime {
                                    WeekDay = (CourseTime.DAYS)(place % 7),
                                    DayTime = (CourseTime.TERM)(place / 7),
                                };
                                string[] beend = temp.Split(
                                    new string[] { "周", "节", " ", "." },
                                    StringSplitOptions.RemoveEmptyEntries);
                                cou.CourseDur = beend[0];
                                if (beend.Length > 3)
                                    cou.CourseDur = String.Format("{0}:{1}", beend[0], beend[1]);
                                if (beend[beend.Length - 1].Equals("S"))
                                    place++;
                                cs.Courses.Add(cou);
                                break;
                        }
                        term++;
                        if (term > 3) {
                            term = term % 4;
                        }
                    }
                }
            }

            cs.Term = DateTime.Now.Month > 8 ?
                String.Format("{0}-{1}", DateTime.Now.Year, DateTime.Now.Year + 1) :
                 String.Format("{0}-{1}", DateTime.Now.Year - 1, DateTime.Now.Year);
            return cs;
        }

        public void UpDateCourse() {
            LoadClassPage();
            courseSet = UpdateCourse();
            XmlHelper.CreatCourseSetNode(courseSet);
        }

        public CourseSet GetCourse() {
            return XmlHelper.GetCourseSetNode(DateTime.Now.Month > 8 ?
                String.Format("{0}-{1}", DateTime.Now.Year, DateTime.Now.Year + 1) :
                 String.Format("{0}-{1}", DateTime.Now.Year - 1, DateTime.Now.Year));
        }
        #endregion


        #region One信息格式


        #endregion
    }

}