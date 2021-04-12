using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Struct
{
    /// <summary>
    /// 应用模块信息
    /// </summary>
    public class ModuleInfo
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
        /// 顺序号
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 首页路由
        /// </summary>
        public string Entrance { get; set; }
        /// <summary>
        /// 所属分类
        /// </summary>
        public string Category { get; set; }
    }
}