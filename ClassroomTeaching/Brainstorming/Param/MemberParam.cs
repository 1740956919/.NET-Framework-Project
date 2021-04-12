using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Param
{
    public class MemberParam
    {
        public MemberParam()
        {
            this.MemberIds = new List<String>();
        }

        /// <summary>
        /// 成员标识列表
        /// </summary>
        public List<String> MemberIds { get; set; }
    }
}