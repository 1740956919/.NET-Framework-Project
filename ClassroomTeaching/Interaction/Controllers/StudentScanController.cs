using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interation.WebAPI.Result;
using System;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Controllers
{
    public class StudentScanController : ApiController
    {
        private readonly IClassService _classService = null;

        public StudentScanController(IClassService classService)
        {
            _classService = classService;
        }

        /// <summary>
        /// 学生注册并登录
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPut]
        public void LoginStudentDevice(String id, [FromBody] StudentLogonParam parameter)
        {
            _classService.LoginStudentDevice(id, parameter.UserDeviceCode);
        }

        /// <summary>
        /// 加入教室上课
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [AvatarAuthorize]
        [HttpPost]
        public StudentAttendClassResult AttendClass(String id, [FromBody] StudentAttendClassParam parameter)
        {
            return _classService.AttendClass(id, parameter);
        }
    }
}
