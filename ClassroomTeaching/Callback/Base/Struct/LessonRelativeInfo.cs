using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Struct
{
    /// <summary>
    /// 课堂报告关联课时信息
    /// </summary>
    public class LessonRelativeInfo
    {
        /// <summary>
        /// 关联教学空间标识
        /// </summary>
        public String RelativeTeachingSpaceId { get; set; }
        /// <summary>
        /// 关联的课时标识
        /// </summary>
        public String RelativeLessonId { get; set; }
    }
}