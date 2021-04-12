using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    /// <summary>
    /// 上课状态信息
    /// </summary>
    public class ClassStateInfo
    {
        /// <summary>
        /// 状态
        /// </summary>
        public String State { get; set; }
        /// <summary>
        /// 行为标识
        /// </summary>
        public String BehaviorId { get; set; }
    }
}