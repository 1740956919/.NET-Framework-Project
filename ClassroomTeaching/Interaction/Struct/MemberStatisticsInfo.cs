using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    /// <summary>
    /// 成员统计信息
    /// </summary>
    public class MemberStatisticsInfo
    {
        /// <summary>
        /// 总人数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 已签到人数
        /// </summary>
        public int JoinedCount { get; set; }
    }
}