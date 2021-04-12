using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Struct
{
    /// <summary>
    /// 授权模块信息
    /// </summary>
    public class AuthorizedModuleInfo
    {
        /// <summary>
        /// 模块Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 模块图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// index顺序
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 首页路由名称
        /// </summary>
        public string Entrance { get; set; }
    }
}