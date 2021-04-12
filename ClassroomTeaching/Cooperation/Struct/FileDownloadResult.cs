using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Struct
{
    /// <summary>
    /// 文件下载结果
    /// </summary>
    public class FileDownloadResult
    {
        /// <summary>
        /// 文件流
        /// </summary>
        public Stream FileStream { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public String FileName { get; set; }
    }
}