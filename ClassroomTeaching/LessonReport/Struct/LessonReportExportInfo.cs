using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Struct
{
    /// <summary>
    /// 课堂报告导出信息
    /// </summary>
    public class LessonReportExportInfo
    {
        /// <summary>
        /// 课时标识
        /// </summary>
        public String LessonId { get; set; }
        /// <summary>
        /// 下载位置
        /// </summary>
        public String DownloadDirectory { get; set; }
    }
}