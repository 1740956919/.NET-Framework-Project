using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Struct
{
    public class TeachingClassInfo
    {
        /// <summary>
        /// 教学班标识
        /// </summary>
        public String TeachingClassId { get; set; }
        /// <summary>
        /// 教学班名称
        /// </summary>
        public String TeachingClassName { get; set; }
        /// <summary>
        /// 学生数量
        /// </summary>
        public Int32 StudentCount { get; set; }
        /// <summary>
        /// 课时列表
        /// </summary>
        public List<LessonInfo> Lessons { get; set; }
    }
}