using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Param
{
    /// <summary>
    /// 通道数据校验参数
    /// </summary>
    public class ChannelProofParam
    {
        public ChannelProofParam()
        {
            this.ChannelIds = new List<string>();
        }

        /// <summary>
        /// 通道标识
        /// </summary>
        public List<string> ChannelIds { get; set; }
        /// <summary>
        /// 课时标识
        /// </summary>
        public string LessonId { get; set; }
    }
}