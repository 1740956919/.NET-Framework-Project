using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Struct
{
    /// <summary>
    /// 课时信息
    /// </summary>
    public class LessonInfo
    {
        /// <summary>
        /// 课时标识
        /// </summary>
        public String LessonId { get; set; }
        /// <summary>
        /// 课时名称
        /// </summary>
        public String LessonName { get; set; }
        /// <summary>
        /// 教学班名称
        /// </summary>
        public String TeachingClassName { get; set; }
        /// <summary>
        /// 教师名称
        /// </summary>
        public String TeacherName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public String State { get; set; }
        /// <summary>
        /// 学生数量
        /// </summary>
        public Int32 StudentCount { get; set; }
    }
}