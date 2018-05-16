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
        public void LoadClassPage() {
            CourseInfo = new HtmlDocument();
            try { CourseInfo.LoadHtml(LoginAgent.Instence.GetData("NEUZhjw")); }
            catch(Exception) {
                MessageService.Instence.ShowError(App.Current.MainWindow,"请检查网络连接,用户信息设置");
            }
        }
        
        public CourseSet GetCourse() {
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
                MessageService.Instence.ShowError(App.Current.MainWindow,"登录过期，请重新登录");
                return null;
            }
            string[] s = content.InnerText.Split('\n');
            List<string> co = new List<string>();
            foreach(string ss in s){
                if (ss.Contains("节") || ss.Contains("&nbsp;") || ss.Contains("学期"))
                    co.Add(ss.Trim());
            }
            cs.Term = co[0];
            co.RemoveRange(0, 3);
            co.RemoveAt(co.Count-1);
            foreach ( string k in co) {
                Debug.Print(k);
            }

            return cs;
        }
        #endregion


        #region One信息格式


        #endregion
    }

}