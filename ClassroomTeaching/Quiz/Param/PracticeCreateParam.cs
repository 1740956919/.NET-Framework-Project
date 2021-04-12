using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Param
{
    /// <summary>
    /// 练习创建参数
    /// </summary>
    public class PracticeCreateParam
    {
        /// <summary>
        /// 课时标识
        /// </summary>
        public string LessonId { get; set; }
        /// <summary>
        /// 题型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 题型名称
        /// </summary>
        public string Title { get; set; }
    }
}