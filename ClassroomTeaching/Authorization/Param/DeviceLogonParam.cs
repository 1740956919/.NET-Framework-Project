using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Param
{
    public class DeviceLogonParam
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        public string LogonName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}