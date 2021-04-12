using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Interface;
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
    public class LessonContentController : ApiController
    {
        private readonly ILessonService _lessonService = null;

        public LessonContentController(ILessonService lessonService )
        {
            _lessonService = lessonService;
        }

        /// <summary>
        /// 获取课时内容
        /// </summary>
        /// <param name="id">课时标识</param>
        /// <returns></returns>
        [HttpGet]
        public LessonContentResult GetLessonContent(String id)
        {
            var token = this.GetToken();
            return _lessonService.GetLessonContent(id, token);
        }
    }
}
