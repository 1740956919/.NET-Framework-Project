using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Param
{
    /// <summary>
    /// 讨论主持参数
    /// </summary>
    public class DiscussionChairParam
    {
        /// <summary>
        /// 是否指定成员
        /// </summary>
        public Boolean IsAssignMember { get; set; }
        /// <summary>
        /// 成员标识
        /// </summary>
        public String MemberId { get; set; }
    }
}