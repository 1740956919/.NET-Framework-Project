using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Result
{
    public class MyDiscussionJoinResult
    {
        /// <summary>
        /// 是否分组
        /// </summary>
        public Boolean HasGrouped { get; set; }
        /// <summary>
        /// 是否准备就绪
        /// </summary>
        public Boolean IsReady { get; set; }
        /// <summary>
        /// 讨论类型
        /// </summary>
        public String DiscussionType { get; set; }
        /// <summary>
        /// 讨论班标识
        /// </summary>
        public String DiscussionId { get; set; }
    }
}