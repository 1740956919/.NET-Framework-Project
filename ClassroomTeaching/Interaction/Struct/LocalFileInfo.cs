using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    /// <summary>
    /// 本地文件信息
    /// </summary>
    public class LocalFileInfo
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public String FullPath { get; set; }
        /// <summary>
        /// 最后一次修改时间
        /// </summary>
        public DateTime LastModifyTime { get; set; }
        /// <summary>
        /// 行为标识
        /// </summary>
        public String BehaviorId { get; set; }
    }
}