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
    public class PracticeReviewController : ApiController
    {
        private IPracticeService _practiceService = null;

        public PracticeReviewController(IPracticeService practiceService)
        {
            _practiceService = practiceService;
        }
    
        /// <summary>
        /// 获取停止练习后的答题信息
        /// </summary>
        /// <param name="id">练习标识</param>
        /// <returns></returns>
        [HttpGet]
        public PracticeReviewResult GetPracticeInfo(string id) 
        {
            return _practiceService.GetPracticeInfo(id);
        }

        /// <summary>
        /// 结束练习
        /// </summary>
        /// <param name="id">练习标识</param>
        /// <param name="receptor">课时标识</param>
        [HttpDelete]
        public void CompletePractice(string id, string receptor)
        {
            _practiceService.CompletePractice(id, receptor);
        }
    }
}
