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
    /// 导入图片控制器
    /// </summary>
    [AvatarAuthorize]
    public class ImportPictureController : ApiController
    {
        private readonly IImportLessonService _importLessonService = null;

        public ImportPictureController(IImportLessonService importLessonService)
        {
            _importLessonService = importLessonService;
        }

        [HttpPost]
        public void ImportPicture(PictureImportParam parameter)
        {
            _importLessonService.ImportPicture(parameter);
        }
    }
}
