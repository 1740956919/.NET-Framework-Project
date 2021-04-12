using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class OverseeController : ApiController
    {
        private readonly IIdeaService _ideaService = null;

        public OverseeController(IIdeaService ideaService)
        {
            _ideaService = ideaService;
        }

        /// <summary>
        /// 获取数据收集结果
        /// </summary>
        /// <param name="id">头脑风暴标识</param>
        /// <returns></returns>
        [HttpPost]
        public BrainstormingInfo GetBrainstormingInfo(string id, BrainstormingInfoGetParam parameter)
        {
            return _ideaService.GetBrainstormingInfo(id, parameter);
        }
    }
}
