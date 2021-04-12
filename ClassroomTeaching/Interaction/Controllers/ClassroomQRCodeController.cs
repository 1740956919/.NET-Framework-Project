using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class ClassroomQRCodeController : ApiController
    {
        private readonly IClassService _classService = null;

        public ClassroomQRCodeController(IClassService classService)
        {
            _classService = classService;
        }

        /// <summary>
        /// 获取教室二维码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public QRCodeInfo Get(String id)
        {
            return _classService.GetQRCodeInfo(id);
        }
    }
}
