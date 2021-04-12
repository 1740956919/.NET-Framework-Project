using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Result
{
    public class PracticeDetailResult
    {
        public PracticeDetailResult(){
            this.PracticeStudentInfos = new List<PracticeStudentInfo>();
        }

        /// <summary>
        /// 是否正在互评中
        /// </summary>
        public bool IsExistMutualEvaluative { get; set; }
        /// <summary>
        /// 互评标识
        /// </summary>
        public String MutualEvaluateId { get; set; }
        /// <summary>
        /// 被选中人的标识
        /// </summary>
        public String StudentTargetId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<PracticeStudentInfo> PracticeStudentInfos { get; set; }
    }
}