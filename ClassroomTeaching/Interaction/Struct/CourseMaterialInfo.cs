using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    public class CourseMaterialInfo
    {
        /// <summary>
        /// 资源标识
        /// </summary>
        public String Id { get; set; }
        /// <summary>
        /// 资料名
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 资源类型
        /// </summary>
        public String Type { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }
        /// <summary>
        /// 是否下发
        /// </summary>
        public Boolean IsOpen { get; set; }
    }
}