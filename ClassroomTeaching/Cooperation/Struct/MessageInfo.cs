using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Struct
{
    /// <summary>
    /// 消息信息
    /// </summary>
    public class MessageInfo
    {
        /// <summary>
        /// 消息标识
        /// </summary>
        public String MessageId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 作者信息
        /// </summary>
        public AuthorInfo Author { get; set; }
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