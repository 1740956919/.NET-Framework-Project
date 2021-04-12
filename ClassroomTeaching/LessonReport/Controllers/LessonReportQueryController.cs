using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Result;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class LessonReportQueryController : ApiController
    {
        private readonly ILessonReportService _lessonReportService = null;

        public LessonReportQueryController(ILessonReportService lessonReportService)
        {
            _lessonReportService = lessonReportService;
        }

        /// <summary>
        /// 查询教学场景下的课时
        /// </summary>
        /// <param name="id">场景标识</param>
        /// <returns></returns>
        [HttpPost]
        public LessonQueryResult QueryLessons(string id,[FromBody] LessonRecordQueryParam parametor)
        {
            return _lessonReportService.QueryLessons(id, parametor);
        }

        /// <summary>
        /// 获取课堂报告数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public List<LessonReportResult> GetLessonReport(string id)
        {
            return _lessonReportService.GetLessonReportById(id);
        }
    }
}
