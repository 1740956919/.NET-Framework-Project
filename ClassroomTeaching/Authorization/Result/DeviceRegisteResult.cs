using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Result
{
    /// <summary>
    /// 终端注册结果
    /// </summary>
    public class DeviceRegisteResult
    {
        public DeviceRegisteResult()
        {
            this.Devices = new List<DeviceInfo>();
        }

        /// <summary>
        /// 是否注册
        /// </summary>
        public bool IsRegisted { get; set; }
        /// <summary>
        /// 终端注册信息
        /// </summary>
        public DeviceRegisteInfo RegistedInfo { get; set; }
        /// <summary>
        /// 设备配置信息列表
        /// </summary>
        public List<DeviceInfo> Devices { get; set; }
    }
}