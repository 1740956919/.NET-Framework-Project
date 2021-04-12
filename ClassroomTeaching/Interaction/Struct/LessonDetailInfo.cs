using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    public class LessonDetailInfo
    {
        /// <summary>
        /// 课程名
        /// </summary>
        public String CourseName { get; set; }
        /// <summary>
        /// 封面
        /// </summary>
        public String Cover { get; set; }
        /// <summary>
        /// 教学班名
        /// </summary>
        public String TeachingClassName { get; set; }
        /// <summary>
        /// 课时名
        /// </summary>
        public String LessonName { get; set; }
        /// <summary>
        /// 课程包路径
        /// </summary>
        public String LessonPackagePath { get; set; }
        /// <summary>
        /// 课时标识
        /// </summary>
        public String LessonId { get; set; }
    }
}