using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Param
{
    /// <summary>
    /// 课程导入开始参数
    /// </summary>
    public class ImportStartParam
    {
        /// <summary>
        /// 课时包路径
        /// </summary>
        public String LessonPackagePath { get; set; }
        /// <summary>
        /// 教室标识
        /// </summary>
        public String ClassroomId { get; set; }
    }
}