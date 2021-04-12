using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Struct
{
    public class MyVoteInfo
    {
        public MyVoteInfo()
        {
            this.VoteOpitions = new List<VoteOptionInfo>();
        }

        /// <summary>
        /// 是否递交投票结果
        /// </summary>
        public Boolean IsSubmitted { get; set; }
        /// <summary>
        /// 投票标识
        /// </summary>
        public  String VoteId { get; set; }
        /// <summary>
        /// 投票的选项
        /// </summary>
        public List<VoteOptionInfo> VoteOpitions { get; set; }
    }
}