using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Result
{
    public class StudentAttendClassResult
    {
        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }
        /// <summary>
        /// 课时名称
        /// </summary>
        public string LessonName { get; set; }
        /// <summary>
        /// 上课时间
        /// </summary>
        public DateTime? StartTime { get; set; }
    }
}