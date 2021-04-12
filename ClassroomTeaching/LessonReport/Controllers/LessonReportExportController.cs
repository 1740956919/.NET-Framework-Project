using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Struct;
using LINDGE.Serialization;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class LessonReportExportController : ApiController
    {
        private readonly IIOProvider _ioProvider = null;
        private readonly ILessonReportService _lessonReportService = null;

        public LessonReportExportController(
            IIOProvider iOProviderService,
            ILessonReportService lessonReportService
            )
        {
            _ioProvider = iOProviderService;
            _lessonReportService = lessonReportService;
        }


        /// <summary>
        /// 导出课堂记录
        /// </summary>
        /// <param name="id">场景标识</param>
        /// <returns></returns>
        [HttpPut]
        public LessonReportExportResult ExportLibrary(string id, [FromBody] LessonReportExportInfo parametor)
        {
            return _lessonReportService.ExportLessonReport(id, parametor);
        }

        /// <summary>
        /// 下载课堂记录包
        /// </summary>
        /// <param name="id">场景标识</param>
        /// <param name="receptor">课时标识</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetLessonReportFile(string id, string receptor)
        {
            var response = new HttpResponseMessage();
            var filePath = _ioProvider.Combine(SchemaNames.FileStorage, RootNames.Export, id, receptor);
            if (_ioProvider.Exists(filePath))
            {
                var fileStream = _ioProvider.Open(filePath, FileMode.Open, FileAccess.Read);
                response.Content = new StreamContent(fileStream);
                response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");          
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentLength = fileStream.Length;              
            }          
             else
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent((new { Code = ErrorCodes.FileNotExist }).ToJSONString())
                });
            }
            return response;
        }

        /// <summary>
        /// 取消导出行为
        /// </summary>
        /// <param name="id">行为标识</param>
        [HttpDelete]
        public void Cancel(string id)
        {
            _lessonReportService.CancelExport(id);
        }
    }
}
