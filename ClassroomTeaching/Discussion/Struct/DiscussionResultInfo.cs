using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Struct
{
    public class DiscussionResultInfo
    {
        /// <summary>
        /// 小组名称
        /// </summary>
        public String GroupName { get; set; }
        /// <summary>
        /// 分组标识
        /// </summary>
        public String GroupId { get; set; }
        /// <summary>
        /// 通道路径
        /// </summary>
        public String ChannelUrl { get; set; }
        /// <summary>
        /// 录屏结果路径
        /// </summary>
        public String ScreenRecordUrl { get; set; }
        /// <summary>
        /// 录播结果路径
        /// </summary>
        public String BroadcastUrl { get; set; }
        /// <summary>
        /// 讨论板标识
        /// </summary>
        public String DiscussBoardId { get; set; }
    }
}