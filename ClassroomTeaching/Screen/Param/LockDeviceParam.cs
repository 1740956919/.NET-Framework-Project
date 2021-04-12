using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Param
{
    public class LockDeviceParam
    {
        public LockDeviceParam()
        {
            this.DeviceIds = new List<string>();
        }

        /// <summary>
        /// 需要锁定的设备标识列表
        /// </summary>
        public List<string> DeviceIds { get; set; }
    }
}