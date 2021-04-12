using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
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
    /// <summary>
    /// 添加课时封面控制器
    /// </summary>
    [AvatarAuthorize]
    public class SyncCloudLessonController : ApiController
    {
        private readonly IImportLessonService _importLessonService = null;

        public SyncCloudLessonController(IImportLessonService importLessonService)
        {
            _importLessonService = importLessonService;
        }

        [HttpPost]
        public void SyncCloudLesson(CloudLessonSyncParam parameter)
        {
            _importLessonService.SyncCloudLesson(parameter);
        }
    }
}
