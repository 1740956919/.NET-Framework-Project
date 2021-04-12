using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Result
{
    /// <summary>
    /// 学生提交的练习答案信息
    /// </summary>
    public class PracticeStudentInfo
    {
        /// <summary>
        /// 学生标识
        /// </summary>
        public string StudentId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// 答案
        /// </summary>
        public string Answer { get; set; }
        /// <summary>
        /// 是否被打分
        /// </summary>
        public bool IsScore { get; set; }
        /// <summary>
        /// 得分
        /// </summary>
        public float Score { get; set; }
    }
}