using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Result
{
    /// <summary>
    /// 学生答案简报信息
    /// </summary>
    public class PracticeBriefResult
    {
        /// <summary>
        /// 答案
        /// </summary>
        public string Answer { get; set; }
        /// <summary>
        /// 选择对应答案的学生数量
        /// </summary>
        public int Count { get; set; }
    }
}