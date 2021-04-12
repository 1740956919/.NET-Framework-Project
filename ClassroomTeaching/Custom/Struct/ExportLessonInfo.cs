using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Struct
{
    public class ExportLessonInfo
    {
        /// <summary>
        /// 教学班标识
        /// </summary>
        public string TeachingClassId { get; set; }
        /// <summary>
        /// 课时标识
        /// </summary>
        public string LessonId { get; set; }
        /// <summary>
        /// 课时名称
        /// </summary>
        public string LessonName { get; set; }
    }
}