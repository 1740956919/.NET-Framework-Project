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
    public class GroupBroadcastController : ApiController
    {
        private readonly IBroadcastService _broadcastService = null;

        public GroupBroadcastController(IBroadcastService broadcastService)
        {
            _broadcastService = broadcastService;
        }

        /// <summary>
        /// 获取广播
        /// </summary>
        /// <param name="id">场景标识</param>
        /// <returns></returns>
        [HttpGet]
        public BroadcastReceiveResult ReceiveBroadcast(string id)
        {
            return _broadcastService.ReceiveBroadcast(id);
        }
    }
}
