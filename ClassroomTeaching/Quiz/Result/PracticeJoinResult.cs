using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Result
{
    public class PracticeJoinResult
    {
        /// <summary>
        /// 题型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 答案
        /// </summary>
        public string Answer { get; set; }
        /// <summary>
        /// 是否提交
        /// </summary>
        public bool IsSubmitted { get; set; }
        /// <summary>
        /// 题目内容
        /// </summary>
        public string Question { get; set; }
    }
}