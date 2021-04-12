using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Const
{
    /// <summary>
    /// 消息常量定义
    /// </summary>
    public class MessageCodes
    {
        #region 消息主题
        /// <summary>
        /// 全班消息
        /// </summary>
        public const string All = "Classroom/All";
        /// <summary>
        /// 私人消息
        /// </summary>
        public const string User = "Classroom/User";
        #endregion

        #region 消息内容
        /// <summary>
        /// 更新课时资料
        /// </summary>
        public const string Material = "Material";
        /// <summary>
        /// 下课
        /// </summary>
        public const string Over = "Over";
        /// <summary>
        /// 师生互动
        /// </summary>
        public const string Interact = "Interact";
        /// <summary>
        /// 结束手机投屏
        /// </summary>
        public const string StopProjection = "StopScreenProjection";
        /// <summary>
        /// 清除消息
        /// </summary>
        public const string Clear = "";
        #endregion
    }
}