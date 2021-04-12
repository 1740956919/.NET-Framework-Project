using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Const
{
    /// <summary>
    /// 行为动作
    /// </summary>
    public class BehaviorActionNames
    {
        /// <summary>
        /// 设置课堂表现
        /// </summary>
        public const string SetPerformance = "SET_PERFORMANCE";
        /// <summary>
        /// 导入课程
        /// </summary>
        public const String ImportLesson = "IMPORTLESSON";
        /// <summary>
        /// 教师扫码
        /// </summary>
        public const String TeacherScan = "TEACHERSCAN";
        /// <summary>
        /// 上课
        /// </summary>
        public const String BeginClass = "BEGINCLASS";
        /// <summary>
        /// 学生签到
        /// </summary>
        public const String StudentSign = "STUDENTSIGN";
        /// <summary>
        /// 互评
        /// </summary>
        public const String MutualScore = "MUTUALSCORE";
        /// <summary>
        /// 随机点名
        /// </summary>
        public const String RandomRollCall = "RANDOM_ROLL_CALL";
        /// <summary>
        /// 抢答
        /// </summary>
        public const String GrabAnswer = "GRAB_ANSWER";
        /// <summary>
        /// 随堂练习
        /// </summary>
        public const String Practice = "CLASSROOM_PRACTICE";
        /// 分组
        /// </summary>
        public const String Grouping = "GROUPING";
        /// <summary>
        /// 分组讨论
        /// </summary>
        public const String GroupDiscuss = "GROUP_DISCUSS";
        /// <summary>
        /// 学生互动独占动作列表
        /// </summary>
        public static List<String> InteractiveAction = new List<String>() { GrabAnswer, RandomRollCall, Practice, MutualScore, Grouping, GroupDiscuss };
        /// <summary>
        /// 协作行为动作列表
        /// </summary>
        public static List<String> CollaborationActions  = new List<String>() { Practice, MutualScore, StudentSign };
        /// <summary>
        /// 下发课程资料
        /// </summary>
        public const String OpenLessonMaterial = "OPEN_LESSON_MATERIAL";
        /// <summary>
        /// 下发本地资料
        /// </summary>
        public const String OpenLocalMaterial = "OPEN_LOCAL_MATERIAL";
        /// <summary>
        /// 预览课程资料
        /// </summary>
        public const String PreviewLessonMaterial = "PREVIEW_LESSON_MATERIAL";
    }
}