using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.WebAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Callback.WebAPI.Controllers
{
    public class ExportActivityPictureController : ApiController, IExportActivityPicture
    {
        private readonly IExportLessonReportService _exportLessonReportService = null;

        public ExportActivityPictureController(IExportLessonReportService exportLessonReportService)
        {
            _exportLessonReportService = exportLessonReportService;
        }

        [HttpPost]
        [AvatarAuthorize]
        public void ExportPictures(PictureExportParam parameter)
        {
            _exportLessonReportService.ExportActivityPictures(parameter);
        }
    }
}
