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
    public class GroupProjectionController : ApiController
    {
        private readonly IReceiveChannelService _receiveChannelService = null;

        public GroupProjectionController(IReceiveChannelService receiveChannelService)
        {
            _receiveChannelService = receiveChannelService;
        }

        /// <summary>
        /// 开始接收手机投屏
        /// </summary>
        /// <param name="id">课时标识</param>
        /// <returns>投屏标识</returns>
        [HttpPut]
        public object StartReceiveProjection(string id)
        {
            return new
            {
                ProjectionId = _receiveChannelService.StartReceiveProjection(id)
            };
        }

        /// <summary>
        /// 获取手机投屏
        /// </summary>
        /// <param name="id">课时标识</param>
        /// <returns></returns>
        [HttpGet]
        public ProjectionResult GetProjection(string id)
        {
            return _receiveChannelService.GetProjection(id);
        }

        /// <summary>
        /// 停止接收手机投屏
        /// </summary>
        /// <param name="id">投屏标识</param>
        [HttpDelete]
        public void StopReceiveProjection(string id)
        {
            _receiveChannelService.StopReceiveProjection(id);
        }
    }
}
