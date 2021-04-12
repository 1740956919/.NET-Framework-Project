using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    /// <summary>
    /// 教学场景信息
    /// </summary>
    public class SceneLessonInfo
    {
        /// <summary>
        /// 教学场景是否活跃
        /// </summary>
        public Boolean IsActive { get; set; }
        /// <summary>
        /// 教学场景标识
        /// </summary>
        public String SceneId { get; set; }
        /// <summary>
        /// 课时标识
        /// </summary>
        public String LessonId { get; set; }
        /// <summary>
        /// 教学场景名
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 是否有教学场景
        /// </summary>
        public Boolean HasScene { get; set; }
    }
}