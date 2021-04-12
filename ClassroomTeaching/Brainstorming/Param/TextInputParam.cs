using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Param
{
    public class TextInputParam
    {
        /// <summary>
        /// 输入内容
        /// </summary>
        public String Input { get; set; }
        /// <summary>
        /// 是否完成
        /// </summary>
        public Boolean IsComplete { get; set; }
    }
}