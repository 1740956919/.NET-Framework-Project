using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Param
{
    public class StudentAttendClassParam
    {
        /// <summary>
        /// 课时标识
        /// </summary>
        public String LessonId { get; set; }
        /// <summary>
        /// 用户显示名
        /// </summary>
        public String DisplayName { get; set; }
        /// <summary>
        /// 头像标识
        /// </summary>
        public String PortraitId { get; set; }
        /// <summary>
        /// 头像文件编码
        /// </summary>
        public String PortraitFile { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public String Gender { get; set; }
        /// <summary>
        /// 用户相关码
        /// </summary>
        public String CorrelativeCode { get; set; }
        /// <summary>
        /// 用户设备唯一码
        /// </summary>
        public String UserDeviceCode { get; set; }
    }
}