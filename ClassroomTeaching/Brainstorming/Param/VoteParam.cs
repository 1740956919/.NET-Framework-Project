using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Param
{
    public class VoteParam
    {
        public VoteParam()
        {
            this.VoteOpitionIds = new List<string>();
        }

        /// <summary>
        /// 投票项标识列表
        /// </summary>
        public List<String> VoteOpitionIds { get; set; }
    }
}