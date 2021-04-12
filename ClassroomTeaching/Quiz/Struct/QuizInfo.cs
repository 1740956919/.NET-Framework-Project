using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Struct
{
    public class QuizInfo
    {
        /// <summary>
        /// 测验标识
        /// </summary>
        public String QuizId { get; set; }
        /// <summary>
        /// 练习题型(见Const.TaskType)
        /// </summary>
        public String Type { get; set; }
    }
}