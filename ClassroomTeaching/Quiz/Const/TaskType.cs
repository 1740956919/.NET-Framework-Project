using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Const
{
    /// <summary>
    /// 题型(行为动作)
    /// </summary>
    public class TaskType
    {
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
        public const string FillBlank = "FILL_BLANK";
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
        public const string Photo = "PHOTO";
        /// <summary>
        /// 口语(暂无)
        /// </summary>
        public const string OralPractice = "ORAL_PRACTICE";
        /// <summary>
        /// 头脑风暴
        /// </summary>
        public const string Brainstorming = "BRAINSTORMING";
        /// <summary>
        /// 协作讨论
        /// </summary>
        public const string Cooperation = "COOPERATION";
        /// <summary>
        /// 预设练习
        /// </summary>
        public const string PreparadPractice = "PREPAREDPRACTICE";
        /// <summary>
        /// 抢答
        /// </summary>
        public const String GrabAnswer = "GRABANSWER";
        /// <summary>
        /// 随机
        /// </summary>
        public const String Random = "RANDOM";
        /// <summary>
        /// 测验类型列表
        /// </summary>
        public static List<String> quizTypes = new List<String>() { GrabAnswer, Random, SingleChoice, MultipleChoice, FillBlank, ShortAnswer, Record, Photo, Brainstorming, Cooperation };
    }
}