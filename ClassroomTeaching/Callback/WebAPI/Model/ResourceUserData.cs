using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Callback.WebAPI.Model
{
    public class ResourceUserData
    {
        public ResourceUserData()
        {
            this.ResourceName = null;
            this.ResourceType = null;
            this.GlobalId = null;
            this.SourceGlobalId = null;
            this.Source = null;
            this.ItemTypeName = null;
            this.ItemTemplate = null;
        }

        /// <summary>
        /// 资源名称
        /// </summary>
        public string ResourceName { get; set; }
        /// <summary>
        /// 资源类型
        /// </summary>
        public string ResourceType { get; set; }
        /// <summary>
        /// 资源全局唯一标识
        /// </summary>
        public string GlobalId { get; set; }
        /// <summary>
        /// 资源是从这个全局唯一标识对应的资源衍生得到的
        /// </summary>
        public string SourceGlobalId { get; set; }
        /// <summary>
        /// 资源来源
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 资源类型显示名
        /// </summary>
        public string ItemTypeName { get; set; }
        /// <summary>
        /// 资源模版
        /// </summary>
        public string ItemTemplate { get; set; }
    }
}