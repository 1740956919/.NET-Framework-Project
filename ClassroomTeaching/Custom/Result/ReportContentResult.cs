using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Result
{
    public class ReportContentResult
    {
        /// <summary>
        /// 记录时间点
        /// </summary>
        public DateTime RecordTime { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 时间点操做
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string PictureUrl { get; set; }
    }
}