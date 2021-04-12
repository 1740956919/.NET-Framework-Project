using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Result
{
    public class SignBeginResult : VerifyResult
    {
        /// <summary>
        /// 签到会话标识
        /// </summary>
        public String SignSessonId { get; set; }
    }
}