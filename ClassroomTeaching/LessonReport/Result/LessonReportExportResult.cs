using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Result
{
    /// <summary>
    /// 课堂报告导出结果
    /// </summary>
    public class LessonReportExportResult
    {
        /// <summary>
        /// 行为标识
        /// </summary>
        public String BehaviorId { get; set; }
        /// <summary>
        /// 生成成功
        /// </summary>
        public Boolean IsCompleted { get; set; }
        /// <summary>
        /// 导出文件名，IsCompleted为true时生效
        /// </summary>
        public String LessonReportName { get; set; }
    }
}