using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    public class LessonMaterialStatus
    {
        /// <summary>
        /// 是否在上课
        /// </summary>
        public Boolean IsInClass { get; set; }
        /// <summary>
        /// 课时标识
        /// </summary>
        public String LessonId { get; set; }
        /// <summary>
        /// 是否有资料
        /// </summary>
        public Boolean HasMaterials { get; set; }
    }
}