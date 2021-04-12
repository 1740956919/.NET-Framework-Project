using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    /// <summary>
    /// 资料基本信息
    /// </summary>
    public class MaterialBasicInfo
    {
        /// <summary>
        /// 资料标识
        /// </summary>
        public String Id { get; set; }
        /// <summary>
        /// 最后修改时间(只有本地资料有)
        /// </summary>
        public DateTime LastModifyTime { get; set; }
        /// <summary>
        /// 资料类型
        /// </summary>
        public String Type { get; set; }
    }
}