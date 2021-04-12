using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    /// <summary>
    /// 场景状态
    /// </summary>
    public class LessonActivityInfo
    {
        /// <summary>
        /// 是否正在上课
        /// </summary>
        public bool IsTeaching { get; set; }
        /// <summary>
        /// 是否正在标注
        /// </summary>
        public bool IsMarking { get; set; }
        /// <summary>
        /// 是否正在投屏
        /// </summary>
        public bool IsScreening { get; set; }
        /// <summary>
        /// 是否正在互动
        /// </summary>
        public bool IsInteracting { get; set; }
        /// <summary>
        /// 是否正在小组讨论
        /// </summary>
        public bool IsGroupDiscussing { get; set; }
        /// <summary>
        /// 是否正在分组
        /// </summary>
        public bool IsGrouping { get; set; }
    }
}