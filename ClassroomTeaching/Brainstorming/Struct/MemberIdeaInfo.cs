using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Struct
{
    public class MemberIdeaInfo
    {
        public MemberIdeaInfo()
        {
            this.Ideas = new List<IdeaInfo>();
        }

        /// <summary>
        /// 成员标识
        /// </summary>
        public String MemberId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public String Portrait { get; set; }
        /// <summary>
        /// 点子列表
        /// </summary>
        public List<IdeaInfo> Ideas { get; set; }
    }
}