using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Result
{
    /// <summary>
    /// 互动结果
    /// </summary>
    public class InteractionResult
    {
        /// <summary>
        /// 互动标识
        /// </summary>
        public string InteractionId { get; set; }
        /// <summary>
        /// 互动状态
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 练习题型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 是否正在上课
        /// </summary>
        public Boolean IsActive { get; set; }
    }
}