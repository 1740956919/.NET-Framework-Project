using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    public class MutualScoreTargetInfo
    {
        public MutualScoreTargetInfo()
        {
            ScoredTarget = new ScoredTargetInfo();
        }
        /// <summary>
        /// 持续时长
        /// </summary>
        public Int32 DuringSeconds { get; set; }
        /// <summary>
        /// 被评价的目标信息
        /// </summary>
        public ScoredTargetInfo ScoredTarget { get; set; }
        /// <summary>
        /// 关联类型
        /// </summary>
        public String RelationType { get; set; }
        /// <summary>
        /// 关联的活动标识
        /// </summary>
        public String RelationId { get; set; }
    }
}