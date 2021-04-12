using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Param;
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
    /// 本地资料控制器
    /// </summary>
    [AvatarAuthorize]
    public class LocalMaterialController : ApiController
    {
        private readonly IMaterialService _materialService = null;

        public LocalMaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        /// <summary>
        /// 下发本地资料
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPut]
        public void Open(String id, LocalMaterialOpenParam parameter)
        {
            _materialService.OpenLocalMaterial(id, parameter);
        }

        /// <summary>
        /// 获取下发的本地资料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public List<LocalMaterial> Get(String id)
        {
            return _materialService.GetOpenedLocalMaterials(id);
        }
    }
}
