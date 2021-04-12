using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Result
{
    /// <summary>
    /// 接收广播结果
    /// </summary>
    public class BroadcastReceiveResult
    {
        /// <summary>
        /// 教学场景标识
        /// </summary>
        public string SceneId { get; set; }
        /// <summary>
        /// 课时标识
        /// </summary>
        public string LessonId { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public Boolean IsShow { get; set; }
        /// <summary>
        /// 屏幕数据通道 
        /// </summary>
        public string DataChannel { get; set; }
    }
}