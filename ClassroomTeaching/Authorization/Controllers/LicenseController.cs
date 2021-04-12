using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Result;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Controllers
{
    public class LicenseController : ApiController
    {
        private readonly IAuthorizeService _authorizaService = null;

        public LicenseController(IAuthorizeService authorizaService)
        {
            _authorizaService = authorizaService;
        }

        /// <summary>
        /// 检查许可证是否有效
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public LicenseCheckResult CheckLicense()
        {
            return _authorizaService.CheckLicense();
        }
    }
}
