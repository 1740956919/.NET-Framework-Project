using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    public class OpenedMaterialInfo
    {
        public OpenedMaterialInfo()
        {
            OpenedMaterials = new List<MaterialBasicInfo>();
        }
        /// <summary>
        /// 下发的资料信息
        /// </summary>
        public List<MaterialBasicInfo> OpenedMaterials { get; set; }
        /// <summary>
        /// 能否下发
        /// </summary>
        public Boolean CanOpen { get; set; }
    }
}