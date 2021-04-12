using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Result
{
    /// <summary>
    /// 随机选人结果
    /// </summary>
    public class RandomCandidateResult
    {
        public RandomCandidateResult()
        {
            this.Student = new StudentInfo();
        }

        /// <summary>
        /// 是否存在互评
        /// </summary>
        public bool IsExistMutualEvaluative { get; set; }
        /// <summary>
        /// 互评标识
        /// </summary>
        public string MutualEvaluateId { get; set; }
        /// <summary>
        /// 随机选中的学生
        /// </summary>
        public StudentInfo Student { get; set; }
    }
}