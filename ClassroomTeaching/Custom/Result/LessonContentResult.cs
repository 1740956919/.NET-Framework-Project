using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Result
{
    public class LessonContentResult
    {
        public LessonContentResult()
        {
            Materials = new List<MaterialInfo>();
        }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }
        /// <summary>
        /// 课时名称
        /// </summary>
        public string LessonName { get; set; }
        /// <summary>
        /// 资料列表
        /// </summary>
        public List<MaterialInfo> Materials { get; set; }
    }
}