using LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Result
{
    /// <summary>
    /// 课堂报告详情
    /// </summary>
    public class LessonReportResult
    {
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime? RecordTime { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public String Title { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public String Category { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public String Image { get; set; }
        /// <summary>
        /// 内容类型
        /// </summary>
        public String ContentType { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public String Content { get; set; }
        /// <summary>
        /// 相关数据
        /// </summary>
        public RelateData RelateData { get; set; }
    }
}