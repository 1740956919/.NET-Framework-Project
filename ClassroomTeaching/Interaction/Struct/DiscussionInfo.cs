using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    /// <summary>
    /// 小组讨论信息
    /// </summary>
    public class DiscussionInfo
    {
        /// <summary>
        /// 讨论标识
        /// </summary>
        public string DiscussionId { get; set; }
        /// <summary>
        /// 讨论类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 讨论状态
        /// </summary>
        public string State { get; set; }
    }
}