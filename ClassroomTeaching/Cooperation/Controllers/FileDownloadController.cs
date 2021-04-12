using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Controllers
{
    /// <summary>
    /// 文件下载控制器
    /// </summary>
    [AvatarAuthorize]
    public class FileDownloadController : ApiController
    {
        private readonly ICooperationService _cooperationService = null;

        public FileDownloadController(ICooperationService cooperationService)
        {
            _cooperationService = cooperationService;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="receptor"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Download(String id, String receptor)
        {
            var response = new HttpResponseMessage();

            Stream fileStream = null;
            var fileName = "";
            try
            {
                var materialDownloadResult = _cooperationService.DownloadFile(id, receptor);
                fileStream = materialDownloadResult.FileStream;
                fileName = materialDownloadResult.FileName;
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
            response.Content.Headers.ContentDisposition.FileName = HttpUtility.UrlEncode(fileName);
            return response;
        }
    }
}
