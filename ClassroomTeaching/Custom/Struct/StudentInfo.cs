using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Struct
{
    public class StudentInfo
    {
        /// <summary>
        /// 抢答所用时间
        /// </summary>
        public int SpendSeconds { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Photo { get; set; }
    }
}