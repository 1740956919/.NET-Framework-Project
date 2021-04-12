using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Struct
{
    /// <summary>
    /// 小组完整信息
    /// </summary>
    public class GroupFullInfo
    {
        public GroupFullInfo()
        {
            Members = new List<MemberBasicInfo>();
        }
        /// <summary>
        /// 小组标识
        /// </summary>
        public String GroupId { get; set; }
        /// <summary>
        /// 小组名
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 成员基本信息列表
        /// </summary>
        public List<MemberBasicInfo> Members { get; set; }
    }
}