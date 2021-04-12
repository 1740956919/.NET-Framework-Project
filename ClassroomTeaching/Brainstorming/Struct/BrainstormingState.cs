using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Struct
{
    public class BrainstormingState
    {
        /// <summary>
        /// 头脑风暴状态
        /// </summary>
        public String State { get; set; }
        /// <summary>
        /// 持续时间
        /// </summary>
        public Int32 DuringSeconds { get; set; }
        /// <summary>
        /// 参与人数
        /// </summary>
        public Int32 JoinCount { get; set; }
        /// <summary>
        /// 总人数
        /// </summary>
        public Int32 TotalCount { get; set; }
    }
}