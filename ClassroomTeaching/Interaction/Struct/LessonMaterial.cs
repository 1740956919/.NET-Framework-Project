using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    public class LessonMaterial
    {
        public LessonMaterial()
        {
            Materials = new List<CourseMaterialInfo>();
        }
        /// <summary>
        /// 课时名
        /// </summary>
        public String LessonName { get; set; }
        /// <summary>
        /// 课程资料信息列表
        /// </summary>
        public List<CourseMaterialInfo> Materials { get; set; }
    }
}