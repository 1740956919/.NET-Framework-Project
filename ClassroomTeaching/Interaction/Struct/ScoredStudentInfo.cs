using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    public class ScoredStudentInfo
    {
        /// <summary>
        /// 被评价学生标识
        /// </summary>
        public String ScoredStudentId { get; set; }
        /// <summary>
        /// 学生头像
        /// </summary>
        public String Portrait { get; set; }
        /// <summary>
        /// 学生名
        /// </summary>
        public String Name { get; set; }
    }
}