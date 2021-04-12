using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class StudentQueryController : ApiController
    {
        private readonly IClassService _classService = null;

        public StudentQueryController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet]
        public List<StudentInfo> Query(String id)
        {
            return _classService.QueryStudents(id);
        }
    }
}
