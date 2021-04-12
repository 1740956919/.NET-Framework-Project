using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Controllers
{
    public class DeviceConfigController : ApiController
    {
        private readonly IConfigService _configService = null;

        public DeviceConfigController(IConfigService configService)
        {
            _configService = configService;
        }

        [HttpGet]
        public object GetRegisteResult()
        {
            var deviceRegisteResult = _configService.GetDeviceRegisteResult();
            return new
            {
                deviceRegisteResult.IsRegisted,
                deviceRegisteResult.RegistedInfo,
                Devices = deviceRegisteResult.Devices.Select(d => new
                {
                    d.Number,
                    d.Type,
                    d.Name,
                    d.IsRegisted,
                    d.Enabled
                }).ToList()
            };
        }

        [HttpPut]
        public object RegisteDevice([FromBody]DeviceRegisteInfo deviceRegisteInfo)
        {
            var accountName = _configService.RegistDevice(deviceRegisteInfo);
            return new
            {
                AccountName = accountName
            };
        }
    }
}
