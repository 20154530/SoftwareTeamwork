using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Text;

namespace SoftwareTeamwork {

    class XmlHelper {
        public enum FluxNodeType { Date, Item }
        public const string WebConfig = @"\Configs\Web-config.xml";
        public const string FluxLog = @"\Configs\FluxLog.xml";
        public const string CourseData = @"\Configs\CourseData.xml";

        public XmlHelper() { }

        public static WebLoginInfSet GetInfWithName(String name) {
            StreamReader reader = new StreamReader(App.RootPath + WebConfig, System.Text.Encoding.UTF8);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(reader);
            reader.Close();

            XmlElement root = xmlDocument.DocumentElement;
            XmlNodeList xnl = root.SelectNodes("/Configs/Web");
            XmlNodeList cxnl = null;

            WebLoginInfSet loginInfSet = new WebLoginInfSet();
            loginInfSet.name = name;

            foreach (XmlNode i in xnl)
                if (i.Attributes["name"].Value.Equals(name)) {
                    cxnl = i.ChildNodes;
                    loginInfSet.NeedLogin = i.Attributes["login"].Value.Equals("true");
                    loginInfSet.Compressed = i.Attributes["gzip"].Value.Equals("true");
                    loginInfSet.Verify = i.Attributes["needverifycode"].Value.Equals("true");
                    loginInfSet.CharSet = i.Attributes["charset"].Value;
                    break;
                }

            if (cxnl != null) {
                foreach (XmlNode i in cxnl) {
                    switch (i.Attributes["type"].Value) {
                        case "uri":
                            loginInfSet.Uris.Add(i.Attributes["uri"].Value);
                            break;
                        case "action":
                            loginInfSet.KeyValuePairs.Add(new KeyValuePair<string, string>
                                (i.Attributes["name"].Value, i.Attributes["value"].Value));
                            break;
                        case "verify":
                            loginInfSet.VerifyCode = i.Attributes["value"].Value;
                            break;
                        case "idcode":
                            loginInfSet.IdCodes = new KeyValuePair<string, string>
                                (i.Attributes["name"].Value, i.Attributes["value"].Value);
                            break;
                        case "cookie":
                            loginInfSet.Cookies.Add(new KeyValuePair<string, string>
                                (i.Attributes["name"].Value, i.Attributes["value"].Value));
                            break;
                    }
                }
                return loginInfSet;
            }
            else return null;
        }

        public static int UpdateWebNodeValue(string loc, Dictionary<string, string> dc) {
            StreamReader reader = new StreamReader(App.RootPath + WebConfig, System.Text.Encoding.UTF8);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(reader);
            reader.Close();

            XmlElement root = xmlDocument.DocumentElement;
            XmlNodeList xnl = root.SelectNodes("/Configs/Web");
            XmlNodeList cxnl = null;

            foreach (XmlNode i in xnl)
                if (i.Attributes["name"].Value.Equals(loc)) {
                    cxnl = i.ChildNodes;
                    break;
                }

            if (cxnl != null) {
                foreach (XmlElement i in cxnl) {
                    if (dc.ContainsKey(i.Attributes["name"].Value))
                        i.SetAttribute("value", dc[i.Attributes["name"].Value]);
                }
                xmlDocument.Save(App.RootPath + WebConfig);
                return 0;
            }
            else return -1;
        }

        /// <summary>
        /// 在主节点中创建一系列子节点，主要用于cookies的本地化
        /// </summary>
        /// <param name="loc">节点的主键名</param>
        /// <param name="pairs">要创建子节点的节点标签名[0]，要创建子节点的属性键值对[1]</param>
        /// <returns></returns>
        public static int CreatWebNode(string loc, string[][] pairs) {
            StreamReader reader = new StreamReader(App.RootPath + WebConfig, System.Text.Encoding.UTF8);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(reader);
            reader.Close();

            XmlElement root = xmlDocument.DocumentElement;
            XmlNodeList xnl = root.SelectNodes("/Configs/Web");
            XmlNode par = null;

            foreach (XmlNode i in xnl)
                if (i.Attributes["name"].Value.Equals(loc)) {
                    par = i;
                    break;
                }

            if (par != null) {
                foreach (string[] pair in pairs) {
                    var node = xmlDocument.CreateElement("Inf");
                    foreach (string s in pair) {
                        string[] x = s.Split(':');
                        node.SetAttribute(x[0], x[1]);
                    }
                    par.AppendChild(node);
                }
                xmlDocument.Save(App.RootPath + WebConfig);
                return 0;
            }
            else return -1;
        }

        /// <summary>
        /// 从主节点中删去一系列相同属性的子节点，主要用于本地cookies的删除
        /// </summary>
        /// <param name="loc">节点的主键名</param>
        /// <param name="dc">要删除的子节点键值名称</param>
        /// <returns></returns>
        public static int DeleteWebNode(string loc, HashSet<string> dc) {
            StreamReader reader = new StreamReader(App.RootPath + WebConfig, System.Text.Encoding.UTF8);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(reader);
            reader.Close();

            XmlElement root = xmlDocument.DocumentElement;
            XmlNodeList xnl = root.SelectNodes("/Configs/Web");
            XmlNode par = null;
            XmlNodeList xnls = null;

            foreach (XmlNode i in xnl)
                if (i.Attributes["name"].Value.Equals(loc)) {
                    par = i;
                    xnls = i.ChildNodes;
                    break;
                }

            if (par != null) {
                for (int i = 0; i < xnls.Count; i++) {
                    if (dc.Contains(xnls[i].Attributes["type"].Value)) {
                        par.RemoveChild(xnls[i]); i--;
                    }
                }
                xmlDocument.Save(App.RootPath + WebConfig);
                return 0;
            }
            else return -1;
        }

        /// <summary>
        /// 在XML文件中创建一个Flux格式节点
        /// </summary>
        /// <param name="label">XML节点标签名称</param>
        /// <param name="pairs">要格式化的信息串</param>
        /// <returns></returns>
        public static int CreatFluxNode(FluxInfo fi) {
            XmlDocument xmlDocument = new XmlDocument();
            using (StreamReader reader = new StreamReader(App.RootPath + FluxLog, System.Text.Encoding.UTF8)) {
                xmlDocument.Load(reader);
            }
            if (fi is null)
                return -1;
            XmlNode Root = xmlDocument.DocumentElement;

            XmlNode LastDate = null;

            var Item = xmlDocument.CreateElement("Item");
            LastDate = Root.LastChild;

            if (Root.ChildNodes.Count == 0 || !LastDate.Attributes["Day"].Value.Equals(fi.InfoTime.Day.ToString())
                || !LastDate.Attributes["Mon"].Value.Equals(fi.InfoTime.Month.ToString())) {
                XmlElement xml = xmlDocument.CreateElement("Date");
                xml.SetAttribute("Year", DateTime.Now.Year.ToString());
                xml.SetAttribute("Mon", DateTime.Now.Month.ToString());
                xml.SetAttribute("Day", DateTime.Now.Day.ToString());
                Root.AppendChild(xml);
            }

            LastDate = Root.LastChild;

            if (LastDate.ChildNodes.Count >= 5)
                LastDate.RemoveChild(LastDate.LastChild);
            Item.SetAttribute("FluxData", fi.FluxData.ToString());
            Item.SetAttribute("Balance", fi.Balance.ToString());
            Item.SetAttribute("InfoTime", fi.InfoTime.ToOADate().ToString());


            LastDate.AppendChild(Item);

            xmlDocument.Save(App.RootPath + FluxLog);
            return 0;
        }

        /// <summary>
        /// 删除一个流量信息节点
        /// </summary>
        /// <param name="label"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int DeleteFluxNode(FluxNodeType label, DateTime date) {
            XmlDocument xmlDocument = new XmlDocument();
            using (StreamReader reader = new StreamReader(App.RootPath + FluxLog, System.Text.Encoding.UTF8)) {
                xmlDocument.Load(reader);
            }

            XmlElement Root = xmlDocument.DocumentElement;

            switch (label) {
                case FluxNodeType.Date:
                    for(int i = 0;i<Root.ChildNodes.Count;i++) {
                        if (Root.ChildNodes[i].Attributes["Mon"].Value.Equals(date.Month.ToString()) &&
                            Root.ChildNodes[i].Attributes["Day"].Value.Equals(date.Day.ToString())) {
                            Root.RemoveChild(Root.ChildNodes[i]);
                            break;
                        }
                    }
                    break;
                case FluxNodeType.Item:
                    XmlNodeList secRoots = null;
                    XmlNode SecRoot = null;
                    foreach (XmlNode xn in Root.ChildNodes) {
                        if (xn.Attributes["Mon"].Value.Equals(date.Month.ToString()) &&
                            xn.Attributes["Day"].Value.Equals(date.Day.ToString())) {
                            SecRoot = xn;
                            secRoots = xn.ChildNodes;
                            break;
                        }
                    }

                    if (SecRoot == null)
                        return -1;
                    else {
                        for (int i = 0; i < secRoots.Count; i++) {
                            if (secRoots[i].Attributes["Mon"].Value.Equals(date.Month.ToString()) &&
                                secRoots[i].Attributes["Day"].Value.Equals(date.Day.ToString())) {
                                SecRoot.RemoveChild(secRoots[i]);i--;
                                
                            }
                        }
                    }
                    break;
            }

            xmlDocument.Save(App.RootPath + FluxLog);
            return 0;
        }

        /// <summary>
        /// 获得一组流量趋势,不够的天数使用初始数据凑齐
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static FluxTrendGroup GetFluxTrendGroup(DateTime begin, DateTime end) {
            FluxTrendGroup ftg = new FluxTrendGroup();
            XmlDocument xmlDocument = new XmlDocument();
            using (StreamReader reader = new StreamReader(App.RootPath + FluxLog, System.Text.Encoding.UTF8)) {
                xmlDocument.Load(reader);
            }

            XmlElement Root = xmlDocument.DocumentElement;
            int i = 0;

            foreach (XmlNode xn in Root.ChildNodes) {
                if ((Convert.ToInt32(xn.Attributes["Mon"].Value) > begin.Month? true: Convert.ToInt32(xn.Attributes["Day"].Value) >= begin.Day) &&
                    Convert.ToInt32(xn.Attributes["Day"].Value) <= end.Day ) {
                    if (i == 7)
                        break;
                    if (xn.HasChildNodes) {
                        XmlNode node = xn.FirstChild;
                        FluxInfo fi = new FluxInfo {
                            FluxData = Convert.ToDouble(node.Attributes["FluxData"].Value),
                            Balance = Convert.ToDouble(node.Attributes["Balance"].Value),
                            InfoTime = DateTime.FromOADate(Convert.ToDouble(node.Attributes["InfoTime"].Value))
                        };
                        ftg.FluxInfos[i] = fi;
                        i++;
                    }

                }
            }

            return ftg;
        }

        /// <summary>
        /// 获得日志中最新的流量数据
        /// </summary>
        /// <returns></returns>
        public static FluxInfo GetLatestFluxInfo() {
            XmlDocument xmlDocument = new XmlDocument();
            using (StreamReader reader = new StreamReader(App.RootPath + FluxLog, System.Text.Encoding.UTF8)) {
                xmlDocument.Load(reader);
            }

            XmlElement Root = xmlDocument.DocumentElement;

            XmlNode lasest = Root.LastChild.LastChild;
            if (lasest is null)
                return null;
            else {
                FluxInfo fi = new FluxInfo() {
                    FluxData = Convert.ToDouble(lasest.Attributes["FluxData"].Value),
                    Balance = Convert.ToDouble(lasest.Attributes["Balance"].Value),
                    InfoTime = DateTime.FromOADate(Convert.ToDouble(lasest.Attributes["InfoTime"].Value))
                };
                return fi;
            }
        }

        /// <summary>
        /// 获得一个学期的课表
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public static CourseSet GetCourseSetNode(string term) {
            StreamReader reader = new StreamReader(App.RootPath + CourseData, Encoding.UTF8);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(reader);
            reader.Close();

            XmlElement root = xmlDocument.DocumentElement;
            XmlNodeList xnl = root.SelectNodes("/Courses/CourseSet");
            XmlNodeList xnls = null;
            if (xnl.Count == 0)
                return null;

            CourseSet courseSet = new CourseSet();

            foreach(XmlNode xn in xnl) 
                if (xn.Attributes["Term"].Value.Equals(term)) {
                    xnls = xn.ChildNodes; break;
                }
            
            if(xnls != null && xnls.Count > 0) {
                courseSet.Term = term;
                courseSet.Courses = new List<Course>();
                foreach (XmlNode xn in xnls) {
                    Course temp = new Course();
                    temp.CourseName = xn.Attributes["Name"].Value;
                    temp.CourseTeacher = xn.Attributes["Teacher"].Value;
                    temp.CourseLoc = xn.Attributes["Loc"].Value;
                    temp.CourseDur = xn.Attributes["Dur"].Value;
                    temp.CourseTime = CourseTime.FromString(xn.Attributes["Time"].Value);

                    courseSet.Courses.Add(temp);
                }
            }

            return courseSet;
        }

        /// <summary>
        /// 将一个学期的课表存储到xml
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public static int CreatCourseSetNode(CourseSet set) {
            StreamReader reader = new StreamReader(App.RootPath + CourseData, Encoding.UTF8);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(reader);
            reader.Close();

            XmlElement root = xmlDocument.DocumentElement;
            XmlNodeList xnl = root.SelectNodes("/Courses/CourseSet");
            foreach(XmlNode xn in xnl) {
                if (xn.Attributes["Term"].Value.Equals(set.Term)) {
                    root.RemoveChild(xn);
                    break;
                }
            }

            XmlElement Courses = xmlDocument.CreateElement("CourseSet");
            Courses.SetAttribute("Term", set.Term);
            root.AppendChild(Courses);

            foreach (Course c in set.Courses) {
                XmlElement NewCourse = xmlDocument.CreateElement("Course");
                NewCourse.SetAttribute("Name", c.CourseName);
                NewCourse.SetAttribute("Teacher", c.CourseTeacher);
                NewCourse.SetAttribute("Loc", c.CourseLoc);
                NewCourse.SetAttribute("Dur", c.CourseDur);
                NewCourse.SetAttribute("Time", c.CourseTime.ToString());
                Courses.AppendChild(NewCourse);
            }

            xmlDocument.Save(App.RootPath + CourseData);
            return 0;
        }

        /// <summary>
        /// 重置所有xml
        /// </summary>
        /// <returns>重置是否成功</returns>
        public static int ResetAll() {
            DeleteWebNode("NEUZhjw", new HashSet<string>() { "cookie" });
            Dictionary<string, string> dc = new Dictionary<string, string> {
                    { "WebUserNO", "" },
                    { "Password", "" },
                    { "Agnomen", "" }};
            UpdateWebNodeValue("NEUZhjw", dc);
            dc = new Dictionary<string, string> {
                    { "password", "" },
                    { "username", "" }};
            UpdateWebNodeValue("NEUIpgw", dc);
            StreamReader reader = new StreamReader(App.RootPath + CourseData, Encoding.UTF8);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(reader);
            reader.Close();

            XmlElement root = xmlDocument.DocumentElement;
            root.RemoveAll();
            xmlDocument.Save(App.RootPath + CourseData);

            reader = new StreamReader(App.RootPath + FluxLog, Encoding.UTF8);
            xmlDocument.Load(reader);
            reader.Close();

            root = xmlDocument.DocumentElement;
            root.RemoveAll();
            xmlDocument.Save(App.RootPath + FluxLog);

            return 0;
        }
    }
}