using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Struct
{
    /// <summary>
    /// 终端配置信息
    /// </summary>
    public class DeviceInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 类型 | 由Const.DeviceType定义
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否注册
        /// </summary>
        public bool IsRegisted { get; set; }
        /// <summary>
        /// 注册的IP地址
        /// </summary>
        public string RegistedIPAddress { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }
    }

    public static class DeviceInfoExtend
    {
        public static DeviceInfo ToDeviceInfo(this Generic.ClassroomTeaching.Scene.Struct.DeviceInfo deviceInfo)
        {
            return new DeviceInfo()
            {
                Enabled = deviceInfo.Enabled,
                IsRegisted = deviceInfo.IsRegisted,
                Name = deviceInfo.Name,
                Number = deviceInfo.Number,
                RegistedIPAddress = deviceInfo.RegistedIPAddress,
                Type = Const.DeviceType.ConvertToAuthorizationDeviceType(deviceInfo.Type)
            };
        }
    }
}