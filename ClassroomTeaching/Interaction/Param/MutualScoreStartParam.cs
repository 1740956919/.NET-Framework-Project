using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Param
{
    /// <summary>
    /// 互评开始参数
    /// </summary>
    public class MutualScoreStartParam
    {
        /// <summary>
        /// 课时标识(非空)
        /// </summary>
        public String ClassroomId { get; set; }
        /// <summary>
        /// 关联类型(非空)
        /// </summary>
        public String RelationType { get; set; }
        /// <summary>
        /// 关联的活动标识(可能为空)
        /// </summary>
        public String RelationId { get; set; }
        /// <summary>
        /// 被评价的目标信息
        /// </summary>
        public ScoredTargetInfo ScoredTarget { get; set; }

        /// <summary>
        /// 检查参数有效行
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(this.ClassroomId))
                throw new ArgumentException(nameof(this.ClassroomId));
            if (!this.ScoredTarget.IsVaild())
                throw new ArgumentException(nameof(this.ScoredTarget));

            return true;
        }
    }
}