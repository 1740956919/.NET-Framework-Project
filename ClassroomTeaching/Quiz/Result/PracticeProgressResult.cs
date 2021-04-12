using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Result
{
    /// <summary>
    /// 练习进度信息
    /// </summary>
    public class PracticeProgressResult
    {
        public PracticeProgressResult()
        {
            WaitAnswerStudents = new List<string>();
        }
        /// <summary>
        /// 回答人数
        /// </summary>
        public int ReplyCount { get; set;}
        /// <summary>
        /// 参与练习的总人数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 练习的持续时间
        /// </summary>
        public int DuringSeconds { get; set; }
        /// <summary>
        /// 待回答的学生
        /// </summary>
        public List<string> WaitAnswerStudents { get; set; }
    }
}