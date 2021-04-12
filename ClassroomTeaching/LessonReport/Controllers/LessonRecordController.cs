using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class LessonRecordController : ApiController
    {
        private readonly ILessonReportService _lessonReportService = null;

        public LessonRecordController(ILessonReportService lessonReportService)
        {
            _lessonReportService = lessonReportService;
        }

        [HttpPut]
        public void Add(string id, [FromBody]LessonReportAddParam parameter)
        {
            _lessonReportService.AddLessonRecord(id, parameter);
        }
    }
}
