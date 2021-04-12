using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    public class LocalMaterial
    {
        /// <summary>
        /// 路径
        /// </summary>
        public String Path { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModifyTime { get; set; }
        /// <summary>
        /// 资料类型
        /// </summary>
        public String Type { get; set; }
    }
}