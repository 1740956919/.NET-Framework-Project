using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Result
{
    public class GrabResult
    {
        /// <summary>
        /// 是否是我抢到的
        /// </summary>
        public bool IsMySelf { get; set; }
        /// <summary>
        /// 抢答成功者
        /// </summary>
        public StudentInfo Winer { get; set; }
    }
}