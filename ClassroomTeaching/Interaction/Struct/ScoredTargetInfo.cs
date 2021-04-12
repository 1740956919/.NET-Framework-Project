using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    /// <summary>
    /// 被评价的目标信息
    /// </summary>
    public class ScoredTargetInfo
    {
        public ScoredTargetInfo()
        {
            Image = String.Empty;
        }

        /// <summary>
        /// 被评价目标标识(非空)
        /// </summary>
        public String ScoredId { get; set; }
        /// <summary>
        /// 评价目标类型
        /// </summary>
        public String TargetType { get; set; }
        /// <summary>
        /// 评价目标头像(可能为空)
        /// </summary>
        public String Image { get; set; }
        /// <summary>
        /// 名称(可能为空)
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 检查参数有效行
        /// </summary>
        /// <returns></returns>
        public bool IsVaild()
        {
            if (string.IsNullOrWhiteSpace(this.ScoredId))
                throw new ArgumentException(nameof(this.ScoredId));
            if (string.IsNullOrWhiteSpace(this.TargetType))
                throw new ArgumentException(nameof(this.TargetType));

            return true;
        }
    }
}