using LINDGE.PARA.Translayer.Static.Picture.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI
{
    /// <summary>
    /// 互动教学配置
    /// </summary>
    public class LessonReportConfigSection
    {
        public const String DefaultSectionName = "Translayer.ClassroomTeaching.LessonReport";
        /// <summary>
        /// 图片预设大小
        /// </summary>
        public PictureSpec PictureSpec { get; set; }
        /// <summary>
        /// 临时教师名称
        /// </summary>
        public String TemporaryTeacherName { get; set; }
    }
}