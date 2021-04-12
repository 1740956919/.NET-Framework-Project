using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Result
{
    /// <summary>
    /// 参与抢答结果
    /// </summary>
    public class GrabAnswerJoinResult
    {
        public GrabAnswerJoinResult()
        {
            this.GrabResult = new GrabResult();
        }

        /// <summary>
        /// 是否抢答到
        /// </summary>
        public bool IsGrabbed { get; set; }       
        /// <summary>
        /// 抢答持续时间（单位毫秒）
        /// </summary>
        public int DuringMillseconds { get; set; }
        /// <summary>
        /// 剩余倒计时长（单位毫秒）
        /// </summary>
        public int LeftMillseconds { get; set; }
        /// <summary>
        /// 抢答结果
        /// </summary>
        public GrabResult GrabResult { get; set; }
    }
}
