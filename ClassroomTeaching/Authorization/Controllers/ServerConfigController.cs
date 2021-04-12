using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Controllers
{
    public class ServerConfigController : ApiController
    {
        private readonly IConfigService _configService = null;

        public ServerConfigController(IConfigService configService)
        {
            _configService = configService;
        }

        [HttpGet]
        public ServiceConfigInfo GetServiceConfig()
        {
            return _configService.GetServiceConfig();
        }

        [HttpPut]
        [AvatarAuthorize]
        public void SetServiceConfig([FromBody]ServiceConfigSetParam parameter)
        {
            _configService.SetServiceConfig(parameter);
        }
    }
}
