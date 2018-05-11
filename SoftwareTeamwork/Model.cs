using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SoftwareTeamwork {
    public class InfoContext : DbContext{
        public DbSet<AccountInfo> AccountInfos { get; set; }
        public DbSet<FlowInfo> FlowInfos { get; set; }
        public DbSet<DataTime> DataTimes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseSet> CourseSets { get; set; }
        public DbSet<CourseInfo> CourseInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=SoftwareTeamwork.WORKDB;Trusted_Connection=True;");
        }
    }

    public class AccountInfo {
        public int AccountInfoId { get; set; }
        public string AccountName { get; set; }
        public List<FlowInfo> FlowInfos { get; set; }
        public List<CourseInfo> CourseInfos { get; set; }
    }

    public class FlowInfo {
        public int FlowInfoId { get; set; }
        public int FlowData { get; set; }
        public DataTime InfoTime { get; set; }

        public int AccountInfoId { get; set; }
        public AccountInfo AccountInfo { get; set; }
    }

    public class CourseInfo {
        public int CourseInfoId { get; set; }
        public List<CourseSet> CourseSets { get; set; }

        public int AccountInfoId { get; set; }
        public AccountInfo AccountInfo { get; set; }
    }

    public class DataTime {
        public int DataTimeId { get; set; }
        private int mon = 0;
        public int Mon {
            get => mon;
            set {
                if (value > 12)
                    mon = 12;
                else if (value < 0)
                    mon = 0;
                else
                    mon = value;
            }
        }
        public int Day { get; set; }
        public int Hour { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mon">月</param>
        /// <param name="day">日</param>
        /// <param name="hour">时</param>
        public DataTime(int mon, int day, int hour) {
            Mon = mon;
            Day = day;
            Hour = hour;
        }
        public DataTime(int mon, int day) {
            Mon = mon;
            Day = day;
        }
    }

    public class CourseSet {
        public int CourseSetId { get; set; }
        public string Term { get; set; }
        public List<Course> Courses { get; set; }

        public int CourseInfoId { get; set; }
        public CourseInfo CourseInfo { get; set; }
    }

    public class Course {
        public enum WeekDay {
            MON, TUE, WED, THU, FRI, SAT, SUN
        }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseTeacher { get; set; }
        public string CourseLoc { get; set; }
        public WeekDay CourseTime { get; set; }
        public int CourseBegin { get; set; }
        public int CourseEnd { get; set; }

        public int CourseSetId { get; set; }
        public CourseSet CourseSet { get; set; }
    }
}
