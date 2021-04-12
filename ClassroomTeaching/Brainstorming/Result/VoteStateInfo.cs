using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Result
{
    public class VoteStateInfo
    {
        public VoteStateInfo()
        {
            this.VoteInfo = new VoteInfo();
        }

        /// <summary>
        /// 是否正在投票
        /// </summary>
        public Boolean IsVoting { get; set; }
        /// <summary>
        /// 投票标识
        /// </summary>
        public String VoteId { get; set; }
        /// <summary>
        /// 投票信息
        /// </summary>
        public VoteInfo VoteInfo { get; set; }
    }
}