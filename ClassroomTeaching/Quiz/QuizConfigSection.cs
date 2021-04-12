using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI
{
    public class QuizConfigSection
    {
        public const String DefaultSectionName = "Translayer.ClassroomTeaching.Quiz";

        /// <summary>
        /// 学生默认头像
        /// </summary>
        public String DefaultStudentPortrait { get; set; }
        /// <summary>
        /// 抢答准备时间
        /// </summary>
        public Int32  LeftMillSeconds { get; set; }
        /// <summary>
        /// 默认倒计时长（单位毫秒）
        /// </summary>
        public Int32 CountDownMillseconds { get; set; }
    }
}