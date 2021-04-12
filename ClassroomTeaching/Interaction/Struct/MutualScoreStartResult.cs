using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    /// <summary>
    /// 互评开始结果
    /// </summary>
    public class MutualScoreStartResult : VerifyResult
    {
        /// <summary>
        /// 互评会话标识
        /// </summary>
        public String ScoreSessonId { get; set; }
    }
}