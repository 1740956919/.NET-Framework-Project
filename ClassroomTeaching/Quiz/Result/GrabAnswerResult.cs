using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Result
{
    /// <summary>
    /// 抢答结果
    /// </summary>
    public class GrabAnswerResult
    {
        public GrabAnswerResult()
        {
            this.Student = new StudentInfo();
        }

        /// <summary>
        /// 抢答是否正在进行
        /// </summary>
        public bool IsGrabbing { get; set; }
        /// <summary>
        /// 准备剩余时间
        /// </summary>
        public int LeftSeconds { get; set; }
        /// <summary>
        /// 抢答持续时间
        /// </summary>
        public int DuringSeconds { get; set; }
        /// <summary>
        /// 抢答成功的学生
        /// </summary>
        public StudentInfo Student { get; set; }


    }
}