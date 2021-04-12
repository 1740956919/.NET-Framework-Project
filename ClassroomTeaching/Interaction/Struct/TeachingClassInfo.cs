using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    /// <summary>
    /// 教学班信息
    /// </summary>
    public class TeachingClassInfo
    {
        /// <summary>
        /// 教学班封面
        /// </summary>
        public String Cover { get; set; }
        /// <summary>
        /// 教学班名称
        /// </summary>
        public String TeachingClassName { get; set; }
        /// <summary>
        /// 是否正在互评
        /// </summary>
        public Boolean IsMutualScoring { get; set; }
        /// <summary>
        /// 被互评的学生标识
        /// </summary>
        public String ScoredUniqueCode { get; set; }
        /// <summary>
        /// 是否在上课
        /// </summary>
        public Boolean IsProcessing { get; set; }

    }
}