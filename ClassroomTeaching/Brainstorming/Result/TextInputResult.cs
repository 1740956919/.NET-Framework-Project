using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Result
{
    public class TextInputResult
    {
        /// <summary>
        /// 是否完成
        /// </summary>
        public Boolean IsCompleted { get; set; }
        /// <summary>
        /// 记录内容输入的行为标识
        /// </summary>
        public string InputContent { get; set; }
    }
}