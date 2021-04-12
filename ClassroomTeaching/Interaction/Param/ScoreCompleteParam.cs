using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Param
{
    /// <summary>
    /// 课堂表现打分参数
    /// </summary>
    public class ScoreCompleteParam
    {
        /// <summary>
        /// 评价项代码
        /// </summary>
        public String Code { get; set; }
        /// <summary>
        /// 评价结果 
        /// </summary>
        public String Evaluation { get; set; }

        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(this.Evaluation))
                throw new ArgumentException(nameof(this.Evaluation));
            if (string.IsNullOrWhiteSpace(this.Code))
                throw new ArgumentException(nameof(this.Code));
            return true;
        }
    }
}