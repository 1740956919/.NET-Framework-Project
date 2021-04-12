using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Result
{
    /// <summary>
    /// 互评进度
    /// </summary>
    public class MutualScoreProgress
    {
        /// <summary>
        /// 参与互评的任人数
        /// </summary>
        public Int32 ScoredCount { get; set; }
        /// <summary>
        /// 有资格参与互评的人数
        /// </summary>
        public Int32 TotalCount { get; set; }
        /// <summary>
        /// 平均分
        /// </summary>
        public float AverageScore { get; set; }
    }
}