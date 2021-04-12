using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class InteractStateController : ApiController
    {
        private readonly IInteractService _interactService = null;

        public InteractStateController(IInteractService interactService)
        {
            _interactService = interactService;
        }


        /// <summary>
        /// 查询当前的上课状态
        /// </summary>
        /// <param name="id">课时标识</param>
        /// <returns></returns>
        [HttpGet]
        public ClassStateResult GetClassState(String id)
        {
            return _interactService.GetClassState(id);
        }
    }
}
