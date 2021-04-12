using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Param
{
    /// <summary>
    /// 服务端配置文件设置参数
    /// </summary>
    public class ServiceConfigSetParam
    {
        /// <summary>
        /// 服务端IP地址
        /// </summary>
        public string ServerIPAddress { get; set; }
        /// <summary>
        /// 许可证文件路径
        /// </summary>
        public string LicenseFilePath { get; set; }
        /// <summary>
        /// 无线路由器的SSID
        /// </summary>
        public string WifiSSID { get; set; }
        /// <summary>
        /// 无线路由器的密码
        /// </summary>
        public string WifiPassword { get; set; }
        /// <summary>
        /// 设备修改结果
        /// </summary>
        public List<DeviceModifyParam> DeviceModifyParams { get; set; }
    }

    /// <summary>
    /// 设备修改信息
    /// </summary>
    public class DeviceModifyParam
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }
    }
}