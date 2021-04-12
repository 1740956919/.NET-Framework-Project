using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Struct
{
    public class VoteResult
    {
        /// <summary>
        /// 分类标识
        /// </summary>
        public String CategoryId { get; set; }
        /// <summary>
        /// 分类标识
        /// </summary>
        public Boolean IsVoted { get; set; }
        /// <summary>
        /// 获得票数
        /// </summary>
        public Int32 VoteCount { get; set; }
    }
}