using LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Param
{
    /// <summary>
    /// 消息发送参数
    /// </summary>
    public class MessageSendParam
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        public String Content { get; set; }
        /// <summary>
        /// 是否是文件
        /// </summary>
        public Boolean IsFile { get; set; }
        /// <summary>
        /// 文件信息
        /// </summary>
        public FileInfo FileInfo { get; set; }
    }
}