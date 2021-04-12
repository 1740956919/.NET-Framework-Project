using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Result
{
    /// <summary>
    /// 互评信息
    /// </summary>
    public class MutualScoreInfo
    {
        /// <summary>
        /// 是否正在互评
        /// </summary>
        public Boolean IsMutualScoring { get; set; }
        /// <summary>
        /// 互评会话标识
        /// </summary>
        public String ScoreSessonId { get; set; }
    }
}