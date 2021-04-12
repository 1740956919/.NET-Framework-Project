using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    /// <summary>
    /// 二维码信息
    /// </summary>
    public class QRCodeInfo
    {
        /// <summary>
        /// 教室标识
        /// </summary>
        public String C { get; set; }
        /// <summary>
        /// wifi的SSID
        /// </summary>
        public String S { get; set; }
        /// <summary>
        /// wifi密码
        /// </summary>
        public String P { get; set; }
        /// <summary>
        /// url地址
        /// </summary>
        public String U { get; set; }
        /// <summary>
        /// 加密方式
        /// </summary>
        public String E { get; set; }
        /// <summary>
        /// 工作模式 0:局域网模式，需要切换Wifi, 1:互联网模式，不需要切换Wifi
        /// </summary>
        public String M { get; set; }
    }
}