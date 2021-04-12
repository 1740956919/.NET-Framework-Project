using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Struct
{
    public class GroupInfo
    {
        /// <summary>
        /// 小组名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 分组标识
        /// </summary>
        public string GroupId { get; set; }
        /// <summary>
        /// 通道路径
        /// </summary>
        public string ChannelUrl { get; set; }
    }
}