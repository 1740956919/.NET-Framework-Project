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
    public class ReportController : ApiController
    {
        private readonly ILessonService _lessonService = null;

        public ReportController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        /// <summary>
        /// 查看上课记录详情
        /// </summary>
        /// <param name="id">课时标识</param>
        /// <returns></returns>
        [HttpGet]
        List<ReportContentResult> QueryReportDetail(string id)
        {
            return _lessonService.QueryReportDetail(id);
        }
    }
}
