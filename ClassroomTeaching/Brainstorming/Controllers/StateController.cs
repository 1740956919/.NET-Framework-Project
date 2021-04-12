using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Interface;
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
    public class StateController : ApiController
    {
        private readonly IIdeaService _ideaService = null;

        public StateController(IIdeaService ideaService)
        {
            _ideaService = ideaService;
        }

        /// <summary>
        /// 获取头脑风暴状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public BrainstormingState GetBrainstormingState(String id)
        {
            return _ideaService.GetBrainstormingState(id);
        }

        /// <summary>
        /// 启动讨论板
        /// </summary>
        /// <param name="id"></param>
        [HttpPut]
        public void StartDiscussionBoard(String id)
        {
            _ideaService.StartDiscussionBoard(id);
        }
    }
}
