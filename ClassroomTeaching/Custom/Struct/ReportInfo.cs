using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Struct
{
    public class ReportInfo
    {
        /// <summary>
        /// 上课时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 下课时间
        /// </summary>
        public DateTime EndTime { get; set; }
    }
}