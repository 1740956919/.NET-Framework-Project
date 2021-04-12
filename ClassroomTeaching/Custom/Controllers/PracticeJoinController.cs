using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Param;
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
    public class PracticeJoinController : ApiController
    {
        private readonly IInteractService _interactService = null;

        public PracticeJoinController(IInteractService interactService)
        {
            _interactService = interactService;
        }

        /// <summary>
        /// 学生加入练习
        /// </summary>
        /// <param name="id">练习标识</param>
        /// <returns></returns>
        [HttpGet]
        public PracticeJoinResult JoinPractice(string id)
        {
            return _interactService.JoinPractice(id);
        }

        /// <summary>
        /// 提交练习答案
        /// </summary>
        /// <param name="id">练习标识</param>
        /// <param name="answer">答案</param>
        [HttpPut]
        public void SubmitAnswer(string id, [FromBody] AnswerSubmitParam submitParam)
        {
            _interactService.SubmitAnswer(id, submitParam.Answer);
        }
    }
}
