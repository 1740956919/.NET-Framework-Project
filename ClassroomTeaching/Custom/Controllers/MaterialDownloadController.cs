using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class MaterialDownloadController : ApiController
    {
        private readonly ILessonService _lessonService = null;

        public MaterialDownloadController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        /// <summary>
        /// 下载资料
        /// </summary>
        /// <param name="id">教学资料标识</param>
        [HttpGet]
        public HttpResponseMessage DownloadMaterial(String id)
        {
            var response = new HttpResponseMessage();
;
            Stream fileStream = null;
            var materialName = "";
            try
            {
                var materialDownloadResult = _lessonService.DownLoadMaterial(id);
                fileStream = materialDownloadResult.MaterialStream;
                materialName = materialDownloadResult.MaterialName;
            }
            catch (FileNotFoundException)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
            response.Content = new StreamContent(fileStream);
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            response.Content.Headers.ContentLength = fileStream.Length;
            response.Content.Headers.ContentDisposition.FileName = materialName;
            return response;
        }
    }
}
