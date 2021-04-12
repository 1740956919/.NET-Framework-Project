using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Param
{
    /// <summary>
    /// 学生名单导入参数
    /// </summary>
    public class StudentImportParam
    {
        /// <summary>
        /// 文件句柄
        /// </summary>
        public String Handle { get; set; }
        /// <summary>
        /// 起始数
        /// </summary>
        public Int32 Index { get; set; }
        /// <summary>
        /// 待处理学生数
        /// </summary>
        public Int32 StudentCount { get; set; }
        /// <summary>
        /// 名单标识
        /// </summary>
        public String CensusId { get; set; }
    }
}
