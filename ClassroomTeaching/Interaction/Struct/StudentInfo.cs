using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    /// <summary>
    /// 学生信息
    /// </summary>
    public class StudentInfo
    {
        /// <summary>
        /// 学生标识
        /// </summary>
        public String StudentId { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public String Portrait { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 学号
        /// </summary>
        public String Number { get; set; }
        /// <summary>
        /// 是否登录
        /// </summary>
        public Boolean IsLogon { get; set; }
        /// <summary>
        /// 是否签到
        /// </summary>
        public Boolean IsSigned { get; set; }
        /// <summary>
        /// 最近活跃时间
        /// </summary>
        public DateTime? LatestActiveTime { get; set; }
    }
}