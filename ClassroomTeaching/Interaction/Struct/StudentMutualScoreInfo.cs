using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    /// <summary>
    /// 互评信息
    /// </summary>
    public class StudentMutualScoreInfo
    {
        public StudentMutualScoreInfo()
        {
            MutualScoreInfo = new MutualScoreTargetInfo();
        }
        /// <summary>
        /// 是否正在互评
        /// </summary>
        public Boolean IsMutualScoring { get; set; }
        /// <summary>
        /// 互评会话标识
        /// </summary>
        public String ScoreSessonId { get; set; }

        /// <summary>
        /// 互评信息，当互评中时值有效
        /// </summary>
        public MutualScoreTargetInfo MutualScoreInfo { get; set; }
    }
}