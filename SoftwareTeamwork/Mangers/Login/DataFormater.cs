﻿using System;
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
using System.Windows;
using Drawing = System.Drawing;
using Color = System.Windows.Media.Color;

namespace SoftwareTeamwork {
    class DataFormater //数据格式化类
    {
        public bool IPGWConnected { get; set; }
        private DateTime date = Properties.Settings.Default.WeekNowSet;
        private FluxInfo ipgwInfo = null;
        public FluxInfo IpgwInfo {
            get {
                return ipgwInfo is null ?
                     GetLocalInfo() is null ?
                     UpdateFlux() :
                     GetLocalInfo() :
                     ipgwInfo;
            }
            set { ipgwInfo = value;  }
        }
        private CourseSet courseSet = null;
        public CourseSet CourseSet {
            get {
                if (!date.Equals(Properties.Settings.Default.WeekNowSet))
                    return GetCourse() is null ?
                       UpdateCourse() :
                       GetCourse();
                return courseSet is null ? //为空则寻找最近的课程列表
                       GetCourse() is null ?//最近课程列表为空则寻找网络
                       UpdateCourse() ://没网，那没辙了
                       GetCourse() :
                       courseSet;
            }
            set { courseSet = value; }
        }
        private HtmlDocument CourseInfo;

        public static DataFormater Instense = new DataFormater();

        public DataFormater() {  }

        #region Ipgw信息格式

        public FluxInfo UpdateFlux() {
            return GetIpgwDataInf(LoginAgent.Instence.GetData("NEUIpgw"));
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
                MessageService.Instence.ShowError(null, "用户名或密码错误");
                IPGWConnected = false;
                return null;
            }
            catch (NullReferenceException) {
                MessageService.Instence.ShowError(null, "网络未连接");
                IPGWConnected = false;
                return null;
            }

            XmlHelper.CreatFluxNode(info);
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

        private FluxInfo GetLocalInfo() {
            return XmlHelper.GetLatestFluxInfo();
        }
        #endregion


        #region 教务处

        private void LoadClassPage() {
            CourseInfo = new HtmlDocument();
            try {
                CourseInfo.LoadHtml(LoginAgent.Instence.GetData("NEUZhjw"));
            }
            catch (Exception) {
                App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => {
                    MessageService.Instence.ShowError(null, "请检查网络连接,用户信息设置");
                }));
            }
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
                    MessageService.Instence.ShowError(null, "登录过期，请重新登录");
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

            XmlHelper.CreatCourseSetNode(cs);
            cs.RemoveNotNow();
            return cs;
        }

        public void UpDateCourse() {
            LoadClassPage();
            courseSet = UpdateCourse();
            XmlHelper.CreatCourseSetNode(courseSet);
        }

        public CourseSet GetCourse() {
            courseSet = XmlHelper.GetCourseSetNode(DateTime.Now.Month > 8 ?
                String.Format("{0}-{1}", DateTime.Now.Year, DateTime.Now.Year + 1) :
                 String.Format("{0}-{1}", DateTime.Now.Year - 1, DateTime.Now.Year));
            if (courseSet is null)
                return null;
            courseSet.RemoveNotNow();
            return courseSet;
        }
        #endregion


        #region 颜色格式化
        public static Color GetMediaColor(Drawing.Color color) {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public static Drawing.Color GetDrawingColor(Color color) {
            return Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }
        #endregion
    }

}