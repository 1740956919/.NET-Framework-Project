using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Param
{
    /// <summary>
    /// 互评信息获取参数
    /// </summary>
    public class MutualScoreInfoGetParam
    {
        /// <summary>
        ///关联类型(非空)
        /// </summary>
        public String RelationType { get; set; }
        /// <summary>
        /// 课时标识
        /// </summary>
        public String LessonId { get; set; }
        /// <summary>
        /// 关联的活动标识(可能为空)
        /// </summary>
        public String RelationId { get; set; }
    }
}