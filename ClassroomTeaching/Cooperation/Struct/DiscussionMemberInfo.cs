using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Struct
{
    public class DiscussionMemberInfo
    {
        public DiscussionMemberInfo()
        {
            GroupMembers = new List<GroupMemberInfo>();
        }
        public List<GroupMemberInfo> GroupMembers { get; set; }
        public String State { get; set; }
    }
}