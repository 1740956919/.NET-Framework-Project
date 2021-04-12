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
    public class LessonMaterialStatusController : ApiController
    {
        private readonly IMaterialService _materialService = null;

        public LessonMaterialStatusController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [HttpGet]
        public LessonMaterialStatus Get(String id)
        {
            return _materialService.GetLessonMaterialStatus(id);
        }
    }
}
