using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    /// <summary>
    /// 互动信息
    /// </summary>
    public class InteractiveInfo
    {
        /// <summary>
        /// 互动标识，即学生互评标识/随堂测验标识/抢答标识，根据类型判断
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 互动类型
        /// </summary>
        public string Type { get; set; }
    }
}