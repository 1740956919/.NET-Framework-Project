using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Const
{
    /// <summary>
    /// 抢答状态
    /// </summary>
    public class GrabAnswerState
    {
        /// <summary>
        /// 抢答中
        /// </summary>
        public const string UnderWay = "GRAB_ANSWERING";
        /// <summary>
        /// 抢答结束
        /// </summary>
        public const string Complete = "GRAB_COMPLETE";
    }
}