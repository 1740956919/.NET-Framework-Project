using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Param
{
    public class ReceiveChannelParam
    {
        /// <summary>
        /// 课时标识
        /// </summary>
        public string LessonId { get; set; }
        /// <summary>
        /// 接收标识
        /// </summary>
        public string ReceiveId { get; set; }
        /// <summary>
        /// 屏幕类型
        /// </summary>
        public string ScreenType { get; set; }
    }
}