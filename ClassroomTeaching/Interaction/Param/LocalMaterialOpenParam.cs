using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Param
{
    /// <summary>
    /// 本地资料下发参数
    /// </summary>
    public class LocalMaterialOpenParam
    {
        /// <summary>
        /// 文件句柄
        /// </summary>
        public String Handle { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public String FullPath { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public String Name { get; set; }
    }
}