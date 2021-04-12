using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class DeviceLockerUpdateController : ApiController
    {
        private readonly IReceiveChannelService _receiveChannelService = null;

        public DeviceLockerUpdateController(IReceiveChannelService receiveChannelService)
        {
            _receiveChannelService = receiveChannelService;
        }

        /// <summary>
        /// 锁定设备更新
        /// </summary>
        /// <param name="id">监控标识</param>
        /// <returns></returns>
        [HttpPost]
        public void UpdateDeviceLocker(string id, [FromBody] LockDeviceParam parameter)
        {
            _receiveChannelService.LockDevice(id, parameter);
        }
    }
}
