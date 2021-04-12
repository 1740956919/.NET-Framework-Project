using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Struct
{
    /// <summary>
    /// 成员基本信息
    /// </summary>
    public class MemberBasicInfo
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
        /// 头像
        /// </summary>
        public String Portrait { get; set; }
    }
}