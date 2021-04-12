using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    public class MaterialDownloadResult
    {
        /// <summary>
        /// 资料文件流
        /// </summary>
        public Stream MaterialStream { get; set; }
        /// <summary>
        /// 资料名
        /// </summary>
        public String MaterialName { get; set; }
    }
}