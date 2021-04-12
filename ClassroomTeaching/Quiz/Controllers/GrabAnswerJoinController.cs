using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class GrabAnswerJoinController : ApiController
    {
        private readonly IGrabAnswerService _grabAnswerService = null;

        public GrabAnswerJoinController(IGrabAnswerService grabAnswerService)
        {
            _grabAnswerService = grabAnswerService;
        }

        /// <summary>
        ///学生端显示抢答信息
        /// </summary>
        /// <param name="id">抢答标识</param>
        /// <returns></returns>
        [HttpGet]
        public GrabAnswerJoinResult ShowGrabAnswer(string id)
        {
            return _grabAnswerService.ShowGrabAnswer(id);
        }

        /// <summary>
        /// 学生参与抢答
        /// </summary>
        /// <param name="id">抢答标识</param>
        /// <returns></returns>
        [HttpPut]
        public GrabResult JoinGrabAnswer(string id)
        {
            return _grabAnswerService.JoinGrabAnswer(id);
        }
    }
}
