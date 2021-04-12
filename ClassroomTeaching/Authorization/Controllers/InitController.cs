using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Param;
using LINDGE.Proxy.WebAPIClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Controllers
{
    public class InitController : ApiController
    {
        private readonly IAuthorizeService _authorizaService = null;
        private readonly IConfigService _configService = null;

        public InitController(IAuthorizeService authorizaService,
            IConfigService configService)
        {
            _authorizaService = authorizaService;
            _configService = configService;
        }

        [HttpPut]
        public void Logon([FromBody] DeviceLogonParam parameter)
        {
            _authorizaService.DeviceLogon(parameter, false);
        }

        [HttpPost]
        [AvatarAuthorize]
        public void Init()
        {
            _configService.Init();
        }
    }
}
