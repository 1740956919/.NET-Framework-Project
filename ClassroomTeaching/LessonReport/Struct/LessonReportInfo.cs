using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Struct
{
    /// <summary>
    /// 存入属性的课时信息
    /// </summary>
    public class LessonReportInfo
    {
        /// <summary>
        /// 课时标识
        /// </summary>
        public String LessonId { get; set; }        
        /// <summary>
        /// 课堂报告下载文件名称
        /// </summary>
        public String LessonReportName { get; set; }
    }
}