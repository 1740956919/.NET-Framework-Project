using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Result;
using LINDGE.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class MyMutualScoreController : ApiController
    {
        private readonly IMutualScoreService _mutualScoreService = null;

        public MyMutualScoreController(IMutualScoreService mutualScoreService)
        {
            _mutualScoreService = mutualScoreService;
        }

        /// <summary>
        /// 验证学生是否完成打分
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public StudentVerifyResult VerifyMyMutualScore(String id)
        {
            return _mutualScoreService.VerifyMyMutualScore(id);
        }

        /// <summary>
        /// 学生打分
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parameter"></param>
        [HttpPut]
        public void Score(String id, [FromBody] StudentScoreParam parameter)
        {
            _mutualScoreService.Score(id, parameter.Score);
        }
    }
}
