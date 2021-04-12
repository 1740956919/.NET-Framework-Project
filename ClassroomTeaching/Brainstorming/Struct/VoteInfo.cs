using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Struct
{
    public class VoteInfo
    {
        public VoteInfo()
        {
            this.VoteResult = new List<VoteResult>();
        }

        /// <summary>
        /// 持续时长，单位秒
        /// </summary>
        public Int32 DuringSeconds { get; set; }
        /// <summary>
        /// 参与人数
        /// </summary>
        public Int32 JoinCount { get; set; }
        /// <summary>
        /// 投票结果
        /// </summary>
        public List<VoteResult> VoteResult { get; set; }
    }
}