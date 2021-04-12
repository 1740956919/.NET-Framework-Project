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
    public class PracticeProgressController : ApiController
    {
        private IPracticeService _practiceService = null;

        public PracticeProgressController(IPracticeService practiceService)
        {
            _practiceService = practiceService;
        }

       
        /// <summary>
        /// 显示练习答题进度
        /// </summary>
        /// <param name="id">练习标识</param>
        /// <returns></returns>
        [HttpGet]
        public PracticeProgressResult GetPracticeProgress(string id)
        {
            return _practiceService.GetPracticeProgress(id);
        }          
    }
}
