using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    /// <summary>
    /// 课程资料信息
    /// </summary>
    public class LessonMaterialInfo
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
        /// 资料类型
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
    }
}