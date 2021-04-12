using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Result;
using LINDGE.Serialization;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class StudentSignController : ApiController
    {
        private readonly IClassService _classService = null;

        public StudentSignController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpPut]
        public SignBeginResult Begin(String id)
        {
            var result = _classService.BeginSign(id);
            if (result.IsSuccess)
            {
                return result;
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent((new { Code = result.ErrorCode, Data = result.ErrorData }).ToJSONString())
                });
            }
        }

        [HttpDelete]
        public void Stop(String id)
        {
            _classService.StopSign(id);
        }
    }
}
