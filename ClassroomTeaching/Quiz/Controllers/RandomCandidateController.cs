using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Struct;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class RandomCandidateController : ApiController
    {
        private readonly IRandomCandidateService _randomCandidateService = null;

        public RandomCandidateController(IRandomCandidateService randomCandidateService)
        {
            _randomCandidateService = randomCandidateService;
        }


        /// <summary>
        /// 创建随机选人
        /// </summary>
        /// <param name="id">课时标识</param>
        /// <returns></returns>
        [HttpPut]
        public object CreateRandomCandidate(string id)
        {
            return new
            {
                RandomRollCallId = _randomCandidateService.CreateRandomCandidate(id)
            };
        }

        /// <summary>
        /// 结束随机
        /// </summary>
        /// <param name="id">随机选人标识</param>
        /// <param name="receptor">课时标识</param>
        [HttpDelete]
        public void CompleteRandomCandidate(string id, string receptor)
        {
            _randomCandidateService.CompleteRandomCandidate(id, receptor);
        }

        /// <summary>
        /// 获取随机选人的结果
        /// </summary>
        /// <param name="id">随机选人标识</param>
        /// <returns></returns>
        [HttpGet]
        public RandomCandidateResult GetRandomCandidate(string id)
        {
            return _randomCandidateService.GetRandomCandidate(id);
        }

        /// <summary>
        /// 获取所有登录的学生
        /// </summary>
        /// <param name="id">课时标识</param>
        /// <returns></returns>
        [HttpPost]
        public List<StudentInfo> GetAllStudents(string id)
        {
            return _randomCandidateService.GetAllStudentsBySceneId(id);
        }
    }
}
