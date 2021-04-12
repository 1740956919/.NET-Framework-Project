using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Struct
{
    public class DisscussResult
    {
        public DisscussResult()
        {
            this.Ideas = new List<IdeaInfo>();
        }

        /// <summary>
        /// 分类标识
        /// </summary>
        public String CategoryId { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 点子列表
        /// </summary>
        public List<IdeaInfo> Ideas { get; set; }
        /// <summary>
        /// 是否投票
        /// </summary>
        public Boolean IsVoted { get; set; }
        /// <summary>
        /// 选票数量
        /// </summary>
        public Int32 VoteCount { get; set; }
    }
}