using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Param
{
    /// <summary>
    /// 屏幕广播更新参数
    /// </summary>
    public class BroadcastUpdateParam
    {
        public BroadcastUpdateParam()
        {
            this.DeviceIds = new List<string>();
        }

        /// <summary>
        /// 是否向所有小组屏广播
        /// </summary>
        public Boolean IsBroadcastAll { get; set; }
        /// <summary>
        /// 要广播的设备标识
        /// </summary>
        public List<string> DeviceIds { get; set; }
    }
}