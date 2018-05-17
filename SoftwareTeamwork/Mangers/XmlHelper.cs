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

        public XmlHelper() {

        }

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
        public static int CreatFluxNode(FluxNodeType label, string[] pairs) {
            XmlDocument xmlDocument = new XmlDocument();
            using (StreamReader reader = new StreamReader(App.RootPath + FluxLog, System.Text.Encoding.UTF8)) {
                xmlDocument.Load(reader);
            }
            if (pairs is null)
                return -1;
            XmlNode Root = xmlDocument.DocumentElement;
            switch (label) {
                case FluxNodeType.Date:
                    var Date = xmlDocument.CreateElement("Date");
                    foreach (string s in pairs) {
                        string[] k = s.Split(':');
                        Date.SetAttribute(k[0], k[1]);
                    }
                    Root.AppendChild(Date);
                    break;
                case FluxNodeType.Item:
                    var Item = xmlDocument.CreateElement("Item");
                    var LastDate = Root.LastChild;
                    foreach (string s in pairs) {
                        string[] k = s.Split(':');
                        Item.SetAttribute(k[0], k[1]);
                    }
                    LastDate.AppendChild(Item);
                    break;
            }

            xmlDocument.Save(App.RootPath + FluxLog);
            return 0;
        }

        public static int DeleteFluxNode() {
            XmlDocument xmlDocument = new XmlDocument();
            using (StreamReader reader = new StreamReader(App.RootPath + FluxLog, System.Text.Encoding.UTF8)) {
                xmlDocument.Load(reader);
            }


            xmlDocument.Save(App.RootPath + FluxLog);
            return 0;
        }

        public static CourseSet GetCourseSetNode(string term) {
            StreamReader reader = new StreamReader(App.RootPath + CourseData, Encoding.UTF8);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(reader);
            reader.Close();

            XmlElement root = xmlDocument.DocumentElement;
            XmlNodeList xnl = root.SelectNodes("/Courses/CourseSet");
            XmlNodeList xnls = null;

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

        //public static int DeleteCourseSetNode(string term) {

        //}

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
    }
}