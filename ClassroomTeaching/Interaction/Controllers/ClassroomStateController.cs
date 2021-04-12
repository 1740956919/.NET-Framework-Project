using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class ClassroomStateController : ApiController
    {
        private readonly IClassService _classService = null;

        public ClassroomStateController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet]
        public ClassroomStateInfo GetClassroomState(String id)
        {
            return _classService.GetClassroomState(id);
        }
    }
}
