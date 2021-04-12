using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    public class CloudLessonInfo
    {
        /// <summary>
        /// 课程名
        /// </summary>
        public String CourseName { get; set; }
        /// <summary>
        /// 教学班名
        /// </summary>
        public String TeachingClassName { get; set; }
        /// <summary>
        /// 课时名
        /// </summary>
        public String LessonName { get; set; }
        /// <summary>
        /// 云端教学空间标识
        /// </summary>
        public String CloudTeachingSpaceId { get; set; }
        /// <summary>
        /// 云端课时标识
        /// </summary>
        public String CloudLessonId { get; set; }
        /// <summary>
        /// 导出时间
        /// </summary>
        public DateTime ExportTime { get; set; }
    }
}