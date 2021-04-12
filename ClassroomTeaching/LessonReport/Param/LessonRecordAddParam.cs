using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Param
{
    /// <summary>
    /// 课堂记录
    /// </summary>
    public class LessonReportAddParam
    {
        /// <summary>
        /// 课堂记录的动作
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 图片handle
        /// </summary>
        public string Picture { get; set; }
        /// <summary>
        /// 课堂记录内容
        /// </summary>
        public string Content { get; set; }
    }
}