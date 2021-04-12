using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Struct
{
    /// <summary>
    /// 测验状态
    /// </summary>
    public class QuizState
    {
        public QuizState()
        {
            QuizInfo = new QuizInfo();
        }
        /// <summary>
        /// 是否正在上课
        /// </summary>
        public Boolean IsOnClass { get; set; }
        /// <summary>
        /// 是否正在测验
        /// </summary>
        public Boolean IsInQuiz { get; set; }
        /// <summary>
        /// 测验信息
        /// </summary>
        public QuizInfo QuizInfo { get; set; }
        /// <summary>
        /// 课时标识
        /// </summary>
        public String LessonId { get; set; }
    }
}