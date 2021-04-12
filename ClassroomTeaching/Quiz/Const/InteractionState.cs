using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Struct
{
    /// <summary>
    /// 互动状态
    /// </summary>
    public class InteractionState
    {
        /// <summary>
        /// 未开始
        /// </summary>
        public const string NotStrat = "NOTSTART";
        /// <summary>
        /// 抢答
        /// </summary>
        public const string GrabAnswer = "GRAB_ANSWER";
        /// <summary>
        /// 随机选人
        /// </summary>
        public const string RollCall = "RANDOM";
        /// <summary>
        /// 练习
        /// </summary>
        public const String Practice = "PRACTICE";
        /// <summary>
        /// 单选
        /// </summary>
        public const string SingleChoice = "SINGLE";
        /// <summary>
        /// 多选
        /// </summary>
        public const string MultipleChoice = "MULTIPLE";
        /// <summary>
        /// 填空
        /// </summary>
        public const string SupplyBlank = "FILL_BLANK";
        /// <summary>
        /// 简答
        /// </summary>
        public const string ShortAnswer = "SHORT_ANSWER";
        /// <summary>
        /// 录音
        /// </summary>
        public const string Record = "RECORD";
        /// <summary>
        /// 拍照
        /// </summary>
        public const string Photograph = "PHOTO";     
        /// <summary>
        /// 口语
        /// </summary>
        public const string OralPractice = "ORAL_PRACTICE";
    }
}