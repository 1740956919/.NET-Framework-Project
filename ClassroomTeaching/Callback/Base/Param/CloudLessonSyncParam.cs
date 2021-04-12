using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Param
{
    /// <summary>
    /// 课时创建参数
    /// </summary>
    public class CloudLessonSyncParam
    {
        /// <summary>
        /// 课时标识
        /// </summary>
        public String LessonId { get; set; }
        /// <summary>
        /// 文件句柄
        /// </summary>
        public String Handle { get; set; }
        /// <summary>
        /// 教室标识
        /// </summary>
        public String ClassroomId { get; set; }
    }
}
