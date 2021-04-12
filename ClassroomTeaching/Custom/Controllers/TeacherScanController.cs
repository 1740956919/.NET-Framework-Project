using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class TeacherScanController : ApiController
    {
        private readonly IScanService _scanService = null;

        public TeacherScanController(IScanService scanService)
        {
            _scanService = scanService;
        }


        /// <summary>
        /// 获取所有可以下载的课时列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost]
        public LessonQueryResult QueryAllDownLoadLessons([FromBody]LessonQueryParam parameter)
        {
            return _scanService.QueryAllDownLoadLessons(parameter);
        }

        /// <summary>
        /// 下载课时并上课
        /// </summary>
        /// <param name="parameter"></param>
        [HttpPut]
        public void DownLoadLessonAndAttendClass([FromBody]TeacherAttendClassParam parameter)
        {
            _scanService.DownLoadLessonAndAttendClass(parameter);
        }
    }
}
