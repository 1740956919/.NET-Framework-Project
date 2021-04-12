using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Result;


namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class GrabAnswerController : ApiController
    {
        private readonly IGrabAnswerService _grabAnswerService = null;

        public GrabAnswerController(IGrabAnswerService grabAnswerService)
        {
            _grabAnswerService = grabAnswerService;
        }
   
        /// <summary>
        /// 创建抢答
        /// </summary>
        /// <param name="id">课时标识</param>   
        /// <returns></returns>
        [HttpPut]
        public object CreateGrabAnswer(string id)
        {
            return new
            {
                GrabAnswerId = _grabAnswerService.CreateGrabAnswer(id)
            };
        }

        /// <summary>
        /// 结束抢答
        /// </summary>
        /// <param name="id">抢答标识</param>
        /// <param name="receptor">课时标识</param>
        [HttpDelete]
        public void CompleteGrabAnswer(string id,string receptor)
        {
            _grabAnswerService.CompleteGrabAnswer(id, receptor);
        }

        /// <summary>
        /// 查询抢答状态
        /// </summary>
        /// <param name="id">抢答标识</param>
        /// <returns></returns>
        [HttpGet]
        public GrabAnswerResult GetGrabAnswer(string id)
        {
            return _grabAnswerService.GetGrabAnswer(id);
        }
    }
}
