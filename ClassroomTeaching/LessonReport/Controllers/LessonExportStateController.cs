using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Struct;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class LessonExportStateController : ApiController
    {
        private readonly ILessonReportService _lessonReportService = null;

        public LessonExportStateController( ILessonReportService lessonRecordExportService )
        {
            _lessonReportService = lessonRecordExportService;
        }
  
        /// <summary>
        /// 课堂报告导出状态
        /// </summary>
        /// <param name="id">场景标识</param>
        /// <returns></returns>
        [HttpGet]
        public LessonReportExportState GetLessonReportExportState(string id)
        {
            return _lessonReportService.GetLessonReportExportState(id);
        }
    }
}
