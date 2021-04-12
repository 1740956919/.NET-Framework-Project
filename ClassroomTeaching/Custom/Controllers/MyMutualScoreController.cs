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
    public class MyMutualScoreController : ApiController
    {
        private readonly ILessonService _lessonService = null;

        public MyMutualScoreController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        /// <summary>
        /// 验证学生是否完成打分
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public VerifyResult VerifyMyMutualScore(String id)
        {
            return _lessonService.VerifyMyMutualScore(id);
        }

        /// <summary>
        /// 学生打分
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parameter"></param>
        [HttpPut]
        public void Score(String id, [FromBody]StudentScoreParam parameter)
        {
            _lessonService.Score(id, parameter.Score);
        }
    }
}
