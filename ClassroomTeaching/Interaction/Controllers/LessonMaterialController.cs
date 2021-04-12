using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Controllers
{
    /// <summary>
    /// 课程资料控制器
    /// </summary>
    [AvatarAuthorize]
    public class LessonMaterialController : ApiController
    {
        private readonly IMaterialService _materialService = null;

        public LessonMaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }
        
        [HttpGet]
        public LessonMaterial Get(String id)
        {
            return _materialService.GetLessonMaterial(id);
        }

        [HttpPut]
        public void Open(String id, String receptor)
        {
            _materialService.OpenLessonMaterial(id, receptor);
        }

        [HttpPost]
        public MaterialPreviewResult Preview(String id)
        {
            return _materialService.PreviewLessonMaterial(id);
        }
    }
}
