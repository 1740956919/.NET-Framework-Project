using LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Result
{
    /// <summary>
    /// 接收投屏结果
    /// </summary>
    public class ProjectionResult
    {
        public ProjectionResult()
        {
            ChannelInfos = new List<ChannelInfo>();
        }

        /// <summary>
        /// 是否存在投屏
        /// </summary>
        public Boolean IsProjection { get; set; }
        /// <summary>
        /// 投屏标识
        /// </summary>
        public string ProjectionId { get; set; }
        /// <summary>
        /// 投屏的通道信息
        /// </summary>
        public List<ChannelInfo> ChannelInfos { get; set; }
    }
}