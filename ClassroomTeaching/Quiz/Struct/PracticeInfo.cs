using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Struct
{
    /// <summary>
    /// 练习回答信息
    /// </summary>
    public class PracticeInfo
    {
        /// <summary>
        /// 回答的人数
        /// </summary>
        public int ReplyCount { get; set; }
        /// <summary>
        /// 参与练习的总人数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 练习所用时间
        /// </summary>
        public int DuringSeconds { get; set; }
    }
}