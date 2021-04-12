using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Struct
{
    /// <summary>
    /// 历史消息信息
    /// </summary>
    public class HistoryMessageInfo
    {
        public HistoryMessageInfo()
        {
            Messages = new List<MessageInfo>();
        }
        /// <summary>
        /// 讨论状态
        /// </summary>
        public String State { get; set; }
        /// <summary>
        /// 主持人标识
        /// </summary>
        public String ChairId { get; set; }
        /// <summary>
        /// 消息列表信息
        /// </summary>
        public List<MessageInfo> Messages { get; set; }
    }
}