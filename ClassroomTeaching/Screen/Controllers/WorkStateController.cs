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
    public class WorkStateController : ApiController
    {
        private readonly IBroadcastService _broadcastService = null;

        public WorkStateController(IBroadcastService broadcastService)
        {
            _broadcastService = broadcastService;
        }

        /// <summary>
        /// 查询屏幕广播状态
        /// </summary>
        /// <param name="id">场景标识</param>
        /// <returns></returns>
        [AvatarAuthorize]
        [HttpGet]
        public WorkStateInfo GetBroadcastWorkState(string id)
        {
            return _broadcastService.GetBroadcastWorkState(id);
        }
    }
}
