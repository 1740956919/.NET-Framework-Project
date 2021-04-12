using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Struct
{
    /// <summary>
    /// 课堂报告导出状态
    /// </summary>
    public class LessonReportExportState
    {
        /// <summary>
        /// 状态
        /// </summary>
        public String State { get; set; }
        /// <summary>
        /// 行为标识
        /// </summary>
        public String BehaviorId { get; set; }
    }
}