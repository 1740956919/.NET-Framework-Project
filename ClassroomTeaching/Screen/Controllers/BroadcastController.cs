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
    public class BroadcastController : ApiController
    {
        private readonly IBroadcastService _broadcastService = null;

        public BroadcastController(IBroadcastService broadcastService)
        {
            _broadcastService = broadcastService;
        }

        /// <summary>
        /// 更新屏幕广播
        /// </summary>
        /// <param name="id">场景标识</param>
        /// <param name="parameter"></param>
        /// <returns>广播标识</returns>
        [HttpPut]
        public object UpdateBroadcast(string id, [FromBody]BroadcastUpdateParam parameter)
        {
            return new
            {
                BroadcastId = _broadcastService.UpdateBroadcast(id, parameter)
            };
        }

        /// <summary>
        /// 停止屏幕广播
        /// </summary>
        /// <param name="id">广播标识</param>
        [HttpDelete]
        public void StopBroadcast(string id)
        {
            _broadcastService.StopBroadcast(id);
        }
    }
}
