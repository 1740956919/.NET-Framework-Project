using Ionic.Zip;
using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct;
using LINDGE.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Controllers
{
    /// <summary>
    /// 导入课程控制器
    /// </summary>
    [AvatarAuthorize]
    public class LessonImportController : ApiController
    {
        private readonly IClassService _classService = null;

        public LessonImportController(IClassService classService)
        {
            _classService = classService;
        }

        /// <summary>
        /// 开始课程导入
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPut]
        public ImportStartResult StartImport(String id, ImportStartParam parameter)
        {
            return  _classService.StartImport(id, parameter);
        }

        /// <summary>
        /// 执行数据导入
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parameter"></param>
        [HttpPost]
        public void ExecuteDataImport(String id, ImportExecuteParam parameter)
        {
            _classService.ExecuteImport(id, parameter.Handle);

        }
        /// <summary>
        /// 停止课程导入
        /// </summary>
        /// <param name="workBehaviorId"></param>
        [HttpDelete]
        public void StopLessonImport(String id)
        {
            _classService.StopImport(id);
        }
    }
}
