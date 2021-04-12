using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    public class MaterialInfo
    {
        /// <summary>
        /// 教学资料标识
        /// </summary>
        public String Id { get; set; }
        /// <summary>
        /// 资料名称
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 资料类型
        /// </summary>
        public String Type { get; set; }
        /// <summary>
        /// 是否支持下载
        /// </summary>
        public Boolean CanDownload { get; set; }
        /// <summary>
        /// 是否支持预览
        /// </summary>
        public Boolean CanPreview { get; set; }
        /// <summary>
        /// 预览路径
        /// </summary>
        public String PreviewUrl { get; set; }
    }
}