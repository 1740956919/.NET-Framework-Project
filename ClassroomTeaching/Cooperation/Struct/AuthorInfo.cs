using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Struct
{
    /// <summary>
    /// 作者信息
    /// </summary>
    public class AuthorInfo
    {
        /// <summary>
        /// 作者标识
        /// </summary>
        public String AuthorId { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public String Portrait { get; set; }
        /// <summary>
        /// 是否是我自己
        /// </summary>
        public Boolean IsMySelf { get; set; }
        /// <summary>
        /// 显示名
        /// </summary>
        public String DisplayName { get; set; }
    }
}