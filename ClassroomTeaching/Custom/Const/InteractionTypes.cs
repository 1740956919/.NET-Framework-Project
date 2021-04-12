using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Const
{
    /// <summary>
    /// 互动操作名常量
    /// </summary>
    public class InteractionTypes
    {
        /// <summary>
        /// 学生互评
        /// </summary>
        public const String MutualScore = "MUTUAL_SCORE";
        /// <summary>
        /// 随堂测验
        /// </summary>
        public const String Practice = "PRACTICE";
        /// <summary>
        /// 抢答
        /// </summary>
        public const String GrabAnswer = "GRAB_ANSWER";
        /// <summary>
        /// 选择分组
        /// </summary>
        public const String ChooseGroup = "CHOOSE_GROUP";
        /// <summary>
        /// 小组讨论
        /// </summary>
        public const String Discussion = "DISCUSSION";
    }
}