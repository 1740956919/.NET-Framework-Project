using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Result
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
        /// 学生名
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 学号
        /// </summary>
        public String Number { get; set; }
        /// <summary>
        /// 分组名
        /// </summary>
        public String GroupName { get; set; }
        /// <summary>
        /// 是否签到
        /// </summary>
        public Boolean IsSigned { get; set; }
        /// <summary>
        /// 学生唯一码
        /// </summary>
        public String UniqueCode { get; set; }
    }
}