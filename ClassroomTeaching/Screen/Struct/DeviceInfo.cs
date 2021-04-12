using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Struct
{
    /// <summary>
    /// 设备信息
    /// </summary>
    public class DeviceInfo
    {
        /// <summary>
        /// 是否已经接收广播画面
        /// </summary>
        public Boolean IsRecevice { get; set; }
        /// <summary>
        /// 设备标识 
        /// </summary>
        public string DeviceId { get; set; }
        /// <summary>
        /// 设备名称 
        /// </summary>
        public string DeviceName { get; set; }
    }
}