using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Const
{
    public class ErrorCodes
    {
        /// <summary>
        /// 文件不存在
        /// </summary>
        public const String LoseFile = "1003";
        /// <summary>
        /// 上课失败
        /// </summary>
        public const String BeginClassError = "1004";
        /// <summary>
        /// 重复上课，上课失败
        /// </summary>
        public const String RepeatBeginClass = "1005";
        /// <summary>
        /// 创建课时失败
        /// </summary>
        public const String LessonCreateError = "1006";
        /// <summary>
        /// 导入课程失败
        /// </summary>
        public const String ImportLesson = "1007";
        /// <summary>
        /// 执行导入失败
        /// </summary>
        public const String ExecuteImport = "1008";
        /// <summary>
        /// 停止导入失败
        /// </summary>
        public const String StopImport = "1009";
        /// <summary>
        /// 课时未开始
        /// </summary>
        public const String LessonNoteOpen = "1010";
        /// <summary>
        /// 教师已存在
        /// </summary>
        public const String LessonExistTeacher = "1011";
        /// <summary>
        /// 独占行为已存在
        /// </summary>
        public const String ExclusiveBehaviorExist = "1012";
        /// <summary>
        /// 互评已停止
        /// </summary>
        public const String MutualScoreStopped = "4004";
        /// <summary>
        /// 无权限加入
        /// </summary>
        public const String JoinWithoutPermission = "4005";
    }
}