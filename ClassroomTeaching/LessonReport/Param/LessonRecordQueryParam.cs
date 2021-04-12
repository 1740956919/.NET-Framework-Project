using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Param
{
    /// <summary>
    /// 课时查询参数
    /// </summary>
    public class LessonRecordQueryParam
    {
        /// <summary>
        /// 页面数据数量
        /// </summary>
        public Int32 PageSize { get; set; }
        /// <summary>
        /// 页面索引
        /// </summary>
        public Int32 PageIndex { get; set; }
        /// <summary>
        /// 排序名称
        /// </summary>
        public String OrderName { get; set; }
    }
}