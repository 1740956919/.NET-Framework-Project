using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class ChannelAliveController : ApiController
    {
        private readonly IReceiveChannelService _receiveChannelService = null;

        public ChannelAliveController(IReceiveChannelService receiveChannelService)
        {
            _receiveChannelService = receiveChannelService;
        }

        /// <summary>
        /// 检查通道是否活跃
        /// </summary>
        /// <param name="id">通道标识</param>
        /// <returns></returns>
        [HttpGet]
        public ChannelAliveResult CheckChannelAlive(string id)
        {
            return _receiveChannelService.CheckChannelAlive(id);
        }
    }
}
