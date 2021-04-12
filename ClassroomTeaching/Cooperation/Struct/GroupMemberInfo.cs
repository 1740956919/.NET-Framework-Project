using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Struct
{
    /// <summary>
    /// 组成员信息
    /// </summary>
    public class GroupMemberInfo
    {
        /// <summary>
        /// 成员标识
        /// </summary>
        public String MemberId { get; set; }
        /// <summary>
        /// 成员名
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 是否是主持人
        /// </summary>
        public Boolean IsChair { get; set; }
    }
}