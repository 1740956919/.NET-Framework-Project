using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Struct
{
    public class ChannelInfo
    {
        /// <summary>
        /// 接收端服务端口
        /// </summary>
        public string Port { get; set; }
        /// <summary>
        /// 接收端IP地址
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 投屏通道显示名
        /// </summary>
        public string Name { get; set; }
    }
}