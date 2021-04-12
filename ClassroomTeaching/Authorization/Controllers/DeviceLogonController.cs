using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Controllers
{
    public class DeviceLogonController : ApiController
    {
        private readonly IAuthorizeService _authorizaService = null;

        public DeviceLogonController(IAuthorizeService authorizaService)
        {
            _authorizaService = authorizaService;
        }

        [HttpPut]
        public void DeviceLogon(DeviceLogonParam param)
        {
            _authorizaService.DeviceLogon(param, true);
        }
    }
}
