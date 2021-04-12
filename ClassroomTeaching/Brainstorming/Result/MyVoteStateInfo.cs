using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Result
{
    public class MyVoteStateInfo
    {
        public MyVoteStateInfo()
        {
            this.VoteInfo = new MyVoteInfo();
        }

        /// <summary>
        /// 是否可以投票
        /// </summary>
        public Boolean CanVote { get; set; }
     
        /// <summary>
        /// 投票信息
        /// </summary>
        public MyVoteInfo VoteInfo { get; set; }
    }
}