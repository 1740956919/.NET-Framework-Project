using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Struct
{
    public class DiscussionStateInfo
    {
        /// <summary>
        /// 进行中的讨论标识
        /// </summary>
        public String DiscussionId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public String State { get; set; }
        /// <summary>
        /// 讨论类型
        /// </summary>
        public String Type { get; set; }     
        /// <summary>
        /// 小组数量
        /// </summary>
        public Int32 GroupCount { get; set; }
        /// <summary>
        /// 是否正在上课
        /// </summary>
        public Boolean IsActive { get; set; }
    }
}