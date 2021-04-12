using LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Result
{
    /// <summary>
    /// 课时查询结果
    /// </summary>
    public class LessonQueryResult
    {
        public LessonQueryResult()
        {
            LessonInfos = new List<LessonInfo>();
        }

        /// <summary>
        /// 数据总数量
        /// </summary>
        public Int32 TotalCount { get; set; }
        /// <summary>
        /// 课时信息
        /// </summary>
        public List<LessonInfo> LessonInfos { get; set; }
    }
}