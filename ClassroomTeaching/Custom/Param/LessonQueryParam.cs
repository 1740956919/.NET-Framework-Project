using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Param
{
    public class LessonQueryParam
    {
        /// <summary>
        /// 用户设备唯一码
        /// </summary>
        public string UserDeviceCode { get; set; }
        /// <summary>
        /// 登录单位标识
        /// </summary>
        public string LogonUnitId { get; set; }
    }
}