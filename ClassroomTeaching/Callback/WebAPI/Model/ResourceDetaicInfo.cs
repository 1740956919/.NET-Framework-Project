using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Callback.WebAPI.Model
{
    public class ResourceDetailInfo
    {
        public ResourceDetailInfo()
        {
            this.ResourceId = null;
            this.Attributes = new Dictionary<string, string>();
            this.UserData = new ResourceUserData();
            this.ResourceUsages = new List<ResourceUsage>();
        }
        /// <summary>
        /// 资源标识
        /// </summary>
        public string ResourceId { get; set; }
        /// <summary>
        /// 资源属性
        /// </summary>
        public Dictionary<string, string> Attributes { get; set; }
        /// <summary>
        /// 资源信息
        /// </summary>
        public ResourceUserData UserData { get; set; }
        /// <summary>
        /// 用途
        /// </summary>
        public List<ResourceUsage> ResourceUsages { get; set; }
        /// <summary>
        /// 关联信息
        /// </summary>
        public ResourceUserData Relation { get; set; }
    }
}