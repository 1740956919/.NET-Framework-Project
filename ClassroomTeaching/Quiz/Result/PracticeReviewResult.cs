using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Result
{
    /// <summary>
    /// 练习汇总信息
    /// </summary>
    public class PracticeReviewResult
    {
        public PracticeReviewResult()
        {
            this.PracticeSummaryInfo = new PracticeInfo();
        }

        /// <summary>
        /// 是否存在简报
        /// </summary>
        public bool IsExistBrief { get; set; }
      
        /// <summary>
        /// 练习信息
        /// </summary>
        public PracticeInfo PracticeSummaryInfo { get; set; }
    }
}