using LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Result
{
    public class DiscussionReviewResult
    {
        public DiscussionReviewResult()
        {
            this.GroupDiscussResults = new List<DiscussionResultInfo>();
        }

        /// <summary>
        /// 讨论类型
        /// </summary>
        public String DiscussType { get; set; }
        /// <summary>
        /// 是否存在互评
        /// </summary>
        public Boolean IsExistMutualEvaluative { get; set; }
        /// <summary>
        /// 小组讨论结果列表
        /// </summary>
        public List<DiscussionResultInfo> GroupDiscussResults { get; set; }
    }
}