using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Struct
{
    /// <summary>
    /// 屏幕广播工作状态
    /// </summary>
    public class WorkStateInfo
    {
        public WorkStateInfo()
        {
            this.GroupScreens = new List<DeviceInfo>();
        }

        /// <summary>
        /// 是否正在广播 
        /// </summary>
        public Boolean IsBroadcast { get; set; }
        /// <summary>
        /// 广播标识
        /// </summary>
        public string BroadcastId { get; set; }
        /// <summary>
        /// 接收广播的设备
        /// </summary>
        public List<DeviceInfo> GroupScreens { get; set; }
        /// <summary>
        /// 是否正在监控屏幕 
        /// </summary>
        public Boolean IsMonitor { get; set; }
        /// <summary>
        /// 监控标识 
        /// </summary>
        public string MonitorId { get; set; }
        /// <summary>
        /// 是否正在接收手机投屏 
        /// </summary>
        public Boolean IsProjection { get; set; }
        /// <summary>
        /// 接收手机投屏标识 
        /// </summary>
        public string ProjectionId { get; set; }
    }
}