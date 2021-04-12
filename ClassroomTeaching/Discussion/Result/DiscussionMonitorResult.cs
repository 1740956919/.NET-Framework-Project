using LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Result
{
    public class DiscussionMonitorResult
    {
        public DiscussionMonitorResult()
        {
            this.Groups = new List<GroupInfo>();
        }

        /// <summary>
        /// 持续时长
        /// </summary>
        public int DuringSeconds { get; set; }
        /// <summary>
        /// 小组屏列表
        /// </summary>
        public List<GroupInfo> Groups { get; set; }
    }
}