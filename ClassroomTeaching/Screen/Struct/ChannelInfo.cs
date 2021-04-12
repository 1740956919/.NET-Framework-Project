using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Struct
{
    /// <summary>
    /// 通道信息
    /// </summary>
    public class ChannelInfo
    {
        /// <summary>
        /// 通道标识
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 屏幕数据通道 
        /// </summary>
        public string DataChannel { get; set; }
        /// <summary>
        /// 是否收到数据
        /// </summary>
        public Boolean HasReceiveData { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 总帧数
        /// </summary>
        public Int32 TotalFrameCount { get; set; }
    }
}