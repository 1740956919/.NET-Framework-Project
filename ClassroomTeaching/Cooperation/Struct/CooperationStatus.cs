using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Struct
{
    /// <summary>
    /// 讨论状态
    /// </summary>
    public class CooperationStatus
    {
        /// <summary>
        /// 状态
        /// </summary>
        public String State { get; set; }
        /// <summary>
        /// 是否有主持人
        /// </summary>
        public Boolean HasChair { get; set; }
        /// <summary>
        /// 主持人标识
        /// </summary>
        public String ChairMemberId{ get; set; }
    }
}