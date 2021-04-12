using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Result
{
    public class LessonQueryResult
    {
        /// <summary>
        /// 教室场景标识
        /// </summary>
        public String SceneId { get; set; }
        /// <summary>
        /// 教学班列表
        /// </summary>
        public List<TeachingClassInfo> TeachingClasses { get; set; }
    }
}