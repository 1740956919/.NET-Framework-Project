using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class ScreenProjectionController : ApiController
    {
        private readonly IReceiveChannelService _receiveChannelService = null;

        public ScreenProjectionController(IReceiveChannelService receiveChannelService)
        {
            _receiveChannelService = receiveChannelService;
        }

        /// <summary>
        /// 获取所有的投屏通道
        /// </summary>
        /// <param name="id">课时标识</param>
        /// <param name="receptor">设备类型</param>
        /// <returns></returns>
        [HttpGet]
        public List<ScreenChannelInfo> GetAllChannels(string id,string receptor)
        {
            return _receiveChannelService.GetAllChannels(id,receptor);
        }
    }
}
