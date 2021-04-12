using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Controllers
{
    public class StudentScanController : ApiController
    {
        private readonly IScanService _scanService = null;

        public StudentScanController(IScanService scanService)
        {
            _scanService = scanService;
        }

        /// <summary>
        /// 学生注册并登录
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPut]
        public StudentLoginResult LoginStudentDevice([FromBody]StudentLoginParam parameter)
        {
            return _scanService.LoginStudentDevice(parameter);
        }

        /// <summary>
        /// 加入教室上课
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [AvatarAuthorize]
        [HttpPost]
        public StudentAttendClassResult AttendClass([FromBody]StudentAttendClassParam parameter)
        {
            return _scanService.AttendClass(parameter);
        }
    }
}
