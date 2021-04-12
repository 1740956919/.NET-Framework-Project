using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    public class LessonContent
    {
        public LessonContent()
        {
            Materials = new List<MaterialInfo>();
        }
        /// <summary>
        /// 教学班名
        /// </summary>
        public String CourseName { get; set; }
        /// <summary>
        /// 课时名
        /// </summary>
        public String LessonName { get; set; }
        /// <summary>
        /// 资料信息列表(包含本地资料和课程资料)
        /// </summary>
        public List<MaterialInfo> Materials { get; set; }
    }
}