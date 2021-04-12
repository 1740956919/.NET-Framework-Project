using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Struct
{
    /// <summary>
    /// 终端注册信息
    /// </summary>
    public class DeviceRegisteInfo
    {
        /// <summary>
        /// 注册的IP地址
        /// </summary>
        public string RegistedIPAddress { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }
    }
}