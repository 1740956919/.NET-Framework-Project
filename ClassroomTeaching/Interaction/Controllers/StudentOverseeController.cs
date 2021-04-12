using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Result;
using System;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class StudentOverseeController : ApiController
    {
        private readonly IClassService _classService = null;

        public StudentOverseeController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet]
        public StudentOverseeResult Oversee(String id)
        {
            return _classService.OverseeStudent(id);
        }
    }
}
