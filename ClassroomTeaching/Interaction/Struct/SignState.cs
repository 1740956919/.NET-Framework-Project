using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    public class SignState
    {
        /// <summary>
        /// 是否在签到中
        /// </summary>
        public Boolean IsSigning { get; set; }
        /// <summary>
        /// 签到会话标识
        /// </summary>
        public String SignSessonId { get; set; }
        /// <summary>
        /// 签到持续时间
        /// </summary>
        public Int32 SignDuringSeconds { get; set; }
    }
}