using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    /// <summary>
    /// 资料展示模块状态信息
    /// </summary>
    public class MaterialDisplayStatus
    {
        public MaterialDisplayStatus()
        {
            Materials = new List<LessonMaterialInfo>();
        }
        /// <summary>
        /// 是否有课程资料
        /// </summary>
        public Boolean HasMaterials { get; set; }
        /// <summary>
        /// 课时名
        /// </summary>
        public String LessonName { get; set; }
        /// <summary>
        /// 课程资料列表
        /// </summary>
        public List<LessonMaterialInfo> Materials { get; set; }
    }
}