using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Result
{
    /// <summary>
    /// 许可证校验结果
    /// </summary>
    public class LicenseCheckResult
    {
        /// <summary>
        /// 校验是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 异常错误
        /// </summary>
        public string Error { get; set; }
    }
}