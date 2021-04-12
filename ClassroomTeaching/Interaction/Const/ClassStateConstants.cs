using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Const
{
    /// <summary>
    /// 上课状态常量
    /// </summary>
    public class ClassStateConstants
    {
        /// <summary>
        /// 未上课
        /// </summary>
        public const String Unstart = "Unstart";
        /// <summary>
        /// 正在同步课时数据（教师扫码）
        /// </summary>
        public const String LessonSynching = "LessonSynching";
        /// <summary>
        /// 正在导入课时数据（导入课时包）
        /// </summary>
        public const String LessonImporting = "LessonImporting";
        /// <summary>
        /// 上课中
        /// </summary>
        public const String InClass = "InClass";
        /// <summary>
        /// 正在上传课时数据（导入课时包）
        /// </summary>
        public const String LessonUploading = "LessonUploading";
    }
}