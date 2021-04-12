using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Result
{
    public class VerifyResult
    {
        public VerifyResult()
        {
            IsSuccess = true;
        }
        /// <summary>
        /// 是否成功
        /// </summary>
        public Boolean IsSuccess { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public String ErrorCode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public String ErrorData { get; set; }
    }
}