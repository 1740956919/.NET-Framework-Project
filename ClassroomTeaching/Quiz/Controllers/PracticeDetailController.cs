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
    public class PracticeDetailController : ApiController
    {
        private readonly IPracticeService _practiceService = null;

        public PracticeDetailController(IPracticeService practiceService)
        {
            _practiceService = practiceService;
        }

        [AvatarAuthorize]
        /// <summary>
        /// 显示随堂练习简报
        /// </summary>
        /// <param name="id">练习标识</param>       
        /// <returns></returns>
        [HttpGet]
        public List<PracticeBriefResult> GetPracticeBrief(string id)
        {
            return _practiceService.GetPracticeBrief(id);
        }

        /// <summary>
        /// 显示随堂练习详情
        /// </summary>
        /// <param name="id">练习标识</param>
        /// <param name="receptor">课时标识</param>
        /// <returns></returns>
        [HttpPost]
        public PracticeDetailResult GetPracticeDetail(string id,string receptor)
        {
            return _practiceService.GetPracticeDetail(id, receptor);
        }      
    }
}
