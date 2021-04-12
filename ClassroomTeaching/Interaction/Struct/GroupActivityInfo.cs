using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    public class GroupActivityInfo
    {
        /// <summary>
        /// 是否接收广播
        /// </summary>
        public bool IsReceiveBroadcast { get; set; }
        /// <summary>
        /// 屏幕数据url
        /// </summary>
        public int BroadcastChannelId { get; set; }
        /// <summary>
        /// 是否正在讨论
        /// </summary>
        public bool IsDiscussing { get; set; }
        /// <summary>
        /// 小组讨论信息
        /// </summary>
        public DiscussionInfo DiscussionInfo { get; set; }
        /// <summary>
        /// 场景信息
        /// </summary>
        public SceneLessonInfo SceneInfo { get; set; }
    }
}