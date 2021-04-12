using LINDGE.PARA.Generic.TeachingSpace.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Struct
{
    /// <summary>
    /// 教学班配置信息
    /// </summary>
    public class TeachingClassConfig
    {
        /// <summary>
        /// 教学班名
        /// </summary>
        public String TeachingClassName { get; set; }
        /// <summary>
        /// 课时名
        /// </summary>
        public String LessonName { get; set; }
        /// <summary>
        /// 教学班封面压缩包位置
        /// </summary>
        public String CoverFile { get; set; }
        /// <summary>
        /// 教学班存储句柄
        /// </summary>
        public String CoverId { get; set; }
        /// <summary>
        /// 课时同步记录
        /// </summary>
        public LessonSyncRecordData LessonSyncRecord { get; set; }
    }
}