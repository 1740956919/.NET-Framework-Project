using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    /// <summary>
    /// 环境信息
    /// </summary>
    public class EnvironmentInfo
    {
        /// <summary>
        /// 重定向路由
        /// </summary>
        public string FigureConfigWebAPI { get; set; }
        /// <summary>
        /// 第二层服务路由，形式为https://192.168.41.87/Translayer/ClassroomTeaching.Interaction/api/
        /// </summary>
        public string InteractionWebAPI { get; set; }
        /// <summary>
        /// 第二层服务路由，形式为https://192.168.41.87/Translayer/ClassroomTeaching.Screen/api/
        /// </summary>
        public string ScreenWebAPI { get; set; }
    }
}