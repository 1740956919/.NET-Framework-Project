using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Struct
{
    public class LessonInfo
    {
        /// <summary>
        /// 课时标识
        /// </summary>
        public String LessonId { get; set; }
        /// <summary>
        /// 课时名称
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 最新更新时间
        /// </summary>
        public DateTime LatestModifyTime { get; set; }
    }
}