using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class MaterialDownloadController : ApiController
    {
        public readonly IMaterialService _materialService = null;

        public MaterialDownloadController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [HttpGet]
        public HttpResponseMessage DownLoad(String id)
        {
            var response = new HttpResponseMessage();

            Stream fileStream = null;
            var materialName = "";
            try
            {
                var materialDownloadResult = _materialService.DownLoadLocalMaterial(id);
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
