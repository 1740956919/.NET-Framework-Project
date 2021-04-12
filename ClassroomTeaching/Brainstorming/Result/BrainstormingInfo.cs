using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Result
{
    public class BrainstormingInfo
    {
        public BrainstormingInfo()
        {
            this.DisscussResults = new List<DisscussResult>();
            this.MemberIdeas = new List<MemberIdeaInfo>();
        }
        /// <summary>
        /// 组成员点子
        /// </summary>
        public List<MemberIdeaInfo> MemberIdeas { get; set; }
        /// <summary>
        /// 讨论结果
        /// </summary>
        public List<DisscussResult> DisscussResults { get; set; }
    }
}