using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class LessonStateController : ApiController
    {
        private readonly IClassService _classService = null;

        public LessonStateController(IClassService classService)
        {
            _classService = classService;
        }
        
        [HttpPut]
        public ClassStartResult BeginClass(String id, ClassStartParam parameter)
        {
            return _classService.BeginClass(id, parameter);
        }

        [HttpDelete]
        public void EndClass(String id)
        {
            _classService.EndClass(id);
        }
    }
}
