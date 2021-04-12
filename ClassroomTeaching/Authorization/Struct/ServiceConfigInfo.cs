using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Struct
{
    /// <summary>
    /// 服务端配置信息
    /// </summary>
    public class ServiceConfigInfo
    {
        public ServiceConfigInfo()
        {
            this.Devices = new List<DeviceInfo>();
        }

        /// <summary>
        /// 服务端IP地址
        /// </summary>
        public string ServerIPAddress { get; set; }
        /// <summary>
        /// 许可证文件名称
        /// </summary>
        public string LicenseFileName { get; set; }
        /// <summary>
        /// 无线路由器的SSID
        /// </summary>
        public string WifiSSID { get; set; }
        /// <summary>
        /// 无线路由器的密码
        /// </summary>
        public string WifiPassword { get; set; }
        /// <summary>
        /// 设备配置信息列表
        /// </summary>
        public List<DeviceInfo> Devices { get; set; }
    }
}