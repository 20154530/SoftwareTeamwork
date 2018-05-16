using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Text;
using HtmlAgilityPack;

namespace SoftwareTeamwork {
    class DataFormater //数据格式化类
    {
        private FluxInfo ipgwInfo;
        public FluxInfo IpgwInfo {
            get { return ipgwInfo is null ? GetIpgwDataInf(LoginAgent.Instence.GetData("NEUIpgw")) : IpgwInfo; }
            set { ipgwInfo = value; }
        }
        private HtmlDocument CourseInfo;
        public static DataFormater Instense = new DataFormater();

        public DataFormater() {

        }

        #region Ipgw信息格式

        public void UpdateFlux() {
            LoginAgent.Instence.SetInfset(XmlHelper.GetInfWithName("NEUIpgw"));
            IpgwInfo = GetIpgwDataInf(LoginAgent.Instence.GetData(null));
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
            if (a > 1000000000)
                return  (a / 1000000000.0);
            if (a > 1000000)
                return (a / 1000000.0);
            if (a > 1000)
                return  a / 1000.0;
            return 0.0;
        }

        private string TimeFormater(string Data)//时间信息格式化 in(s)
        {
            Int64 a = 0;
            try {
                a = Convert.ToInt64(Data);
            }
            catch (FormatException ex) {
                MessageService.Instence.ShowError(null, "数据格式错误，请检查用户名和密码");
            }
            double h = Math.Round(a / 3600.0);
            double m = Math.Round((a % 3600) / 60.0);
            double s = a % 3600 % 60;
            return String.Format("{0}:{1}:{2}", h, m, s);
        }

        #endregion


        #region 教务处
        public void LoadClassPage() {
            CourseInfo = new HtmlDocument();
            LoginAgent.Instence.SetInfset(XmlHelper.GetInfWithName("NEUZhjw"));
            CourseInfo.Load(LoginAgent.Instence.GetDataAsStream(null));
        }
        
        private CourseSet GetCourse() {
            CourseSet cs = new CourseSet();
            CourseInfo.get
            return cs;
        }
        #endregion


        #region One信息格式


        #endregion
    }



    //#region =====================课程表==========================
    //enum Day { MON, TUE, WED, THU, FRI, SAT, SUN };//表示是星期几

    //enum Time { First, Second, Third, Fourth, Fifth, Sixth }//表示是第几节课

    ////使用方法:
    ////1. 通过InitializeByFile 或 InitializeByString 初始化
    ////2. 设置Week的值即可从 ClassInfoToShow 课程列表
    //class GetClassInfo
    //{
    //    public static GetClassInfo getClassInfo = new GetClassInfo();

    //    private string infoSource;//需要解析的信息
    //    private string infoTemplate;//做好标记的模板
    //                                //模板标记要求:
    //                                //
    //                                //只需标记在每格第一个数据的位置标记 数据标记
    //                                //数据标记需完全替代数据出现位置,不可添加额外的空格或其他符号
    //                                //在</td>前标记数据结束标记
    //                                //在模板的最开头标记数据结束标记
    //                                //
    //                                //对于长度会变化的部分的信息也要加上开始和结束标记,再在infoClik中去除这些无用信息即可

    //    private char infoSymbol = '@', infoEndSymbol = '$';//标记
    //    private List<int> infoPosition = new List<int>();//数据标记的位置 
    //    private List<int> infoEndPosition = new List<int>();//数据结束标记的位置
    //    private List<int> infoOffset = new List<int>();//上一个数据结束标记距离下一个数据标记的距离.
    //    private List<string> infoClip = new List<string>();//获得的信息片段

    //    private ClassInformation classInformation = new ClassInformation();
    //    private ObservableCollection<Course> classInfoToshow;
    //    public ObservableCollection<Course> ClassInfoToShow { get { return classInfoToshow; } }

    //    private int week;
    //    public int Week
    //    {
    //        get { return week; }
    //        set
    //        {
    //            week = value;
    //            classInfoToshow = GetClassInfo.getClassInfo.classInformation.SortInfoToShow(week);
    //        }
    //    }

    //    //传入包含网站源代码的txt文件的路径
    //    public void InitializeByFile(string path, int weekNumber = 1)
    //    {

    //        GetClassInfo.getClassInfo.infoSource = System.IO.File.ReadAllText(path, Encoding.Default);
    //        Initialize(weekNumber);
    //    }
    //    //传入包含网站源代码的字符串
    //    public void InitializeByString(string source, int weekNumber = 1)
    //    {
    //        infoSource = source;
    //        Initialize(weekNumber);
    //    }
    //    //初始化
    //    private void Initialize(int weekNumber = 1)
    //    {
    //        GetClassInfo.getClassInfo.infoTemplate = System.IO.File.ReadAllText(@"D:\DB\template.txt", Encoding.Default);
    //        GetClassInfo.getClassInfo.SetLabelPosition(infoSymbol, infoEndSymbol);
    //        GetClassInfo.getClassInfo.SetInfoOffset();
    //        GetClassInfo.getClassInfo.GetInfoClip();
    //        GetClassInfo.getClassInfo.GetClassInformatonFormInfoClip();

    //        classInfoToshow = GetClassInfo.getClassInfo.classInformation.SortInfoToShow(weekNumber);
    //    }
    //    //构造函数
    //    private GetClassInfo()
    //    {

    //    }


    //    //传入模板中作为信息标记,和结束标记
    //    //计算数据标识符和数据结束符的位置
    //    private void SetLabelPosition(char infoSymbol, char infoEndSymbol)
    //    {
    //        for (int index = 0; index < infoTemplate.Length; ++index)
    //        {
    //            if (infoTemplate[index] == infoSymbol)
    //            {
    //                infoPosition.Add(index);
    //            }
    //            if (infoTemplate[index] == infoEndSymbol)
    //            {
    //                infoEndPosition.Add(index);
    //            }

    //        }

    //    }

    //    //计算信息之间的偏移量
    //    //
    //    private void SetInfoOffset()
    //    {

    //        //按道理Math.Min(infoPosition.Count,infoEndPosition.Count)) 结果应该为infoEndPosition
    //        for (int index = 0; index < Math.Min(infoPosition.Count, infoEndPosition.Count); ++index)
    //        {
    //            //因为在文件开头设置了infoEndPosition 标志,占用了一个位置,则offset比信息中的信息多一
    //            if (index == 0) infoOffset.Add(infoPosition[index] - infoEndPosition[index] - 1);
    //            else infoOffset.Add(infoPosition[index] - infoEndPosition[index]);
    //        }

    //    }
    //    //获取的结果,#表示一天,##表示到下一时段
    //    //从头到尾的数据顺序为 星期一第一节课 .星期二第一节课.....
    //    //星期一第二节课....
    //    private void GetInfoClip()
    //    {
    //        int position = 0;
    //        bool isInLabel = false;
    //        int day = 1;

    //        const int numberOfUselessInfo = 4;//无用信息的数量,根据html设置

    //        for (int index = 0; index < infoOffset.Count; index++)
    //        {
    //            position += infoOffset[index];
    //            infoClip.Add("");
    //            while (true)            //一个数据标记一直到</td>,模板中的</td>前有结束标记
    //            {
    //                //处理当前position 的字符
    //                if (isInLabel) ;
    //                else infoClip[infoClip.Count - 1] += infoSource[position];

    //                //判断之后字符并决定如何处理之后字符
    //                if (IsPositionBeforeBr(position))
    //                {

    //                    infoClip.Add("");   //换行标签,则目前的clip结束
    //                    isInLabel = true; //下一个字符开始在标签中
    //                }
    //                if (IsPositionLabelEnd(position))
    //                {
    //                    isInLabel = false; //下一个字符开始不在标签中
    //                }

    //                if (!IsPositionBeforeTdEnd(position))//当前整条未结束
    //                {
    //                    ++position;
    //                }
    //                else
    //                {

    //                    //只为有用的信息(即可课表信息)添加 # ,##标记,
    //                    if (infoClip.Count > numberOfUselessInfo)
    //                    {
    //                        ++day;//每次</td>为一天的课程
    //                        infoClip.Add("#");
    //                    }
    //                    if (day >= 8)
    //                    {
    //                        day = 1;
    //                        infoClip.Add("##");
    //                    }
    //                    break;
    //                }

    //            }

    //        }

    //        infoClip.RemoveRange(0, numberOfUselessInfo);//这些信息是html中的会变化的无用的信息
    //    }
    //    //检测infoSource的position处字符之后是否为>
    //    //
    //    private bool IsPositionBeforeLabelStart(int position)
    //    {
    //        return (infoSource[position + 1] == '<');
    //    }
    //    //检测infoSource的position处字符之后是否为>
    //    //
    //    private bool IsPositionBeforeLabelEnd(int position)
    //    {
    //        return (infoSource[position + 1] == '>');
    //    }
    //    //检测infoSource的position处字符之后是否为<br
    //    //
    //    private bool IsPositionBeforeBr(int position)
    //    {
    //        return (infoSource[position + 1] == '<' && infoSource[position + 2] == 'b' && infoSource[position + 3] == 'r');
    //    }
    //    //检测infoSource的position处字符之后是否为</td
    //    //
    //    private bool IsPositionBeforeTdEnd(int position)
    //    {
    //        return (infoSource[position + 1] == '<' && infoSource[position + 2] == '/' && infoSource[position + 3] == 't' && infoSource[position + 4] == 'd');
    //    }
    //    //检测当前位置是否为>
    //    //
    //    private bool IsPositionLabelEnd(int position)
    //    {
    //        return infoSource[position] == '>';
    //    }
    //    private void GetClassInformatonFormInfoClip()
    //    {
    //        int day = (int)Day.MON;          //星期
    //        int time = (int)Time.First;     //时段

    //        int number = 0;

    //        for (int index = 0; index < infoClip.Count; ++index)
    //        {
    //            if (infoClip[index] == "&nbsp;")
    //            {
    //                //这条信息没有保存课程;

    //            }
    //            else if (infoClip[index] == "#")
    //            {
    //                //这个星期的这一时段没有更多课程
    //                ++day;
    //            }
    //            else if (infoClip[index] == "##")
    //            {
    //                //一周的这个时段结束了
    //                //重置为星期一,进入下一时段
    //                day = (int)Day.MON;
    //                ++time;
    //            }
    //            else
    //            {   //为课程信息
    //                ++number;
    //                if (number == 4)
    //                {
    //                    classInformation.AddClassInfo((Day)day, (Time)time,
    //                                                    new Course(infoClip[index - 3],
    //                                                               infoClip[index - 2],
    //                                                               infoClip[index - 1],
    //                                                               infoClip[index]));
    //                    number = 0;
    //                }
    //            }

    //        }

    //    }
    //}
    //class ClassInformation
    //{
    //    private Dictionary<Day, Dictionary<Time, List<Course>>> classInfo = new Dictionary<Day, Dictionary<Time, List<Course>>>();

    //    //修改已经存在的课程信息
    //    //day星期,time时段,index为同时段课程序号
    //    public bool SetClassInfo(Day day, Time time, int index, Course course)
    //    {
    //        if (classInfo.ContainsKey(day))
    //        {
    //            if (classInfo[day].ContainsKey(time))
    //            {
    //                if (classInfo[day][time].Count - 1 >= index)
    //                {
    //                    classInfo[day][time][index] = course;
    //                    return true;
    //                }
    //                else
    //                {
    //                    return false;
    //                }
    //            }
    //            else
    //            {
    //                return false;
    //            }
    //        }
    //        else
    //        {
    //            return false;
    //        }


    //    }
    //    //添加新的课程信息
    //    public void AddClassInfo(Day day, Time time, Course course)
    //    {
    //        //创建空间
    //        if (classInfo.ContainsKey(day))
    //        {
    //            if (classInfo[day].ContainsKey(time))
    //            {

    //            }
    //            else
    //            {
    //                classInfo[day].Add(time, new List<Course>());
    //            }
    //        }
    //        else
    //        {
    //            Dictionary<Time, List<Course>> timeCourseDic = new Dictionary<Time, List<Course>>();
    //            List<Course> courseList = new List<Course>();
    //            timeCourseDic.Add(time, courseList);
    //            classInfo.Add(day, timeCourseDic);
    //        }
    //        //添加
    //        classInfo[day][time].Add(course);
    //    }
    //    //得到课程信息
    //    public bool GetClassInfo(Day day, Time time, int index, Course course)
    //    {
    //        if (classInfo.ContainsKey(day))
    //        {
    //            if (classInfo[day].ContainsKey(time))
    //            {
    //                if (classInfo[day][time].Count - 1 >= index)
    //                {

    //                    classInfo[day][time][index].CopyCourseTo(course);
    //                    return true;
    //                }
    //                else
    //                {
    //                    return false;
    //                }
    //            }
    //            else
    //            {
    //                return false;
    //            }
    //        }
    //        else
    //        {
    //            return false;
    //        }

    //    }

    //    public ObservableCollection<Course> SortInfoToShow(int weekNumber)
    //    {
    //        ObservableCollection<Course> classInfoListToShow = new ObservableCollection<Course>();
    //        for (int day = 0; day < 7; day++)
    //        {
    //            //一整天都没课
    //            if (!classInfo.ContainsKey((Day)day))
    //            {
    //                for (int time = 0; time < 6; time++)
    //                {
    //                    classInfoListToShow.Add(new Course());
    //                }
    //                continue;
    //            }
    //            //可能有课
    //            for (int time = 0; time < 6; time++)
    //            {
    //                //一定没课,补上空的
    //                if (!classInfo[(Day)day].ContainsKey((Time)time) || classInfo[(Day)day][(Time)time].Count == 0)
    //                {
    //                    classInfoListToShow.Add(new Course());
    //                    continue;
    //                }
    //                //可能有课
    //                int classInThisWeek = 0;
    //                for (int index = 0; index < classInfo[(Day)day][(Time)time].Count; index++)
    //                {
    //                    //如果有课,将课程填入toShow
    //                    if (classInfo[(Day)day][(Time)time][index].weekList.Contains(weekNumber))
    //                    {
    //                        classInfoListToShow.Add(classInfo[(Day)day][(Time)time][index]);
    //                        ++classInThisWeek;
    //                    }
    //                }
    //                //确实没课
    //                if (classInThisWeek == 0)
    //                {
    //                    classInfoListToShow.Add(new Course());
    //                }
    //            }
    //        }
    //        return classInfoListToShow;
    //    }


    //}
    ////class Course //单课程信息
    ////{

    ////    //名字
    ////    public string name { get; set; }
    ////    //教师
    ////    public string teacher { get; set; }
    ////    //教室

    ////    public string classroom { get; set; }
    ////    //周
    ////    public string week { get; set; }

    ////    //从字符串中抽取出的离散的week
    ////    public List<int> weekList = new List<int>();
    ////    public Course()
    ////    {
    ////        name = null;
    ////        teacher = null;
    ////        classroom = null;
    ////        week = null;
    ////    }

    ////    public Course(string name, string teacher, string classroom, string week)
    ////    {
    ////        this.name = name;
    ////        this.teacher = teacher;
    ////        this.classroom = classroom;
    ////        this.week = week;
    ////        MakeWeekList();
    ////    }
    ////    public void CopyCourseTo(Course course)
    ////    {
    ////        course.name = this.name;
    ////        course.teacher = this.teacher;
    ////        course.week = this.week;
    ////        course.classroom = this.classroom;
    ////        course.weekList = this.weekList;
    ////    }
    ////    private void MakeWeekList()
    ////    {
    ////        int weekNumber = 0;
    ////        bool isSingleWeek = true;
    ////        for (int index = 0; index < week.Length; index++)
    ////        {
    ////            if (week[index] == '周')//结束
    ////            {
    ////                if (isSingleWeek) weekList.Add(weekNumber);
    ////                else
    ////                {
    ////                    for (int indexInWeek = weekList[weekList.Count - 1] + 1; indexInWeek <= weekNumber; indexInWeek++)
    ////                    {
    ////                        weekList.Add(indexInWeek);
    ////                    }
    ////                }
    ////                isSingleWeek = true;
    ////                weekNumber = 0;
    ////                break;
    ////            }
    ////            else if (week[index] == '.')//
    ////            {

    ////                if (isSingleWeek) weekList.Add(weekNumber);
    ////                else
    ////                {
    ////                    for (int indexInWeek = weekList[weekList.Count - 1] + 1; indexInWeek <= weekNumber; indexInWeek++)
    ////                    {
    ////                        weekList.Add(indexInWeek);
    ////                    }
    ////                }
    ////                isSingleWeek = true;
    ////                weekNumber = 0;

    ////            }
    ////            else if (week[index] == '-')
    ////            {

    ////                isSingleWeek = false;

    ////                weekList.Add(weekNumber);
    ////                weekNumber = 0;
    ////            }
    ////            else
    ////            {
    ////                weekNumber = (Convert.ToInt32(week[index]) - 48) + weekNumber * 10;
    ////            }

    ////        }

    ////    }
    ////}
    //#endregion
}
