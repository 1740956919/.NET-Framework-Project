using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    /// <summary>
    /// 课堂表现信息
    /// </summary>
    public class PerformanceSetInfo
    {
        /// <summary>
        /// 课时标识
        /// </summary>
        public string LessonId { get; set; }

        /// <summary>
        /// 被评价目标标识列表
        /// </summary>
        public List<string> TargetIds { get; set; }
        /// <summary>
        /// 被评价目标类型 | 必填 由常量Const.TargetType定义
        /// </summary>
        public string TargetType { get; set; }
        /// <summary>
        /// 课堂表现 | 必填,由常量Performance定义
        /// </summary>
        public string Performance { get; set; }
        /// <summary>
        /// 课堂表现分类 | 必填
        /// </summary>
        public string PerformanceCategory { get; set; }
    }
}