using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Param
{
    /// <summary>
    /// 评价开始参数
    /// </summary>
    public class ScoreStartParam
    {
        /// <summary>
        /// 被评价目标标识列表(非空)
        /// </summary>
        public List<String> TargetIds { get; set; }
        /// <summary>
        /// 评价目标类型
        /// </summary>
        public String TargetType { get; set; }
        public bool IsVaild()
        {
            if (this.TargetIds == null || this.TargetIds.Count == 0 || this.TargetIds.Any(t => string.IsNullOrWhiteSpace(t)))
                throw new ArgumentException(nameof(this.TargetIds));
            if (string.IsNullOrWhiteSpace(this.TargetType))
                throw new ArgumentException(nameof(this.TargetType));

            return true;
        }
    }
}