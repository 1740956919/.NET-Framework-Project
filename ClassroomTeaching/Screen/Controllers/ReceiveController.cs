using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Param;
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
    public class ReceiveController : ApiController
    {
        private readonly IReceiveChannelService _receiveChannelService = null;

        public ReceiveController(IReceiveChannelService receiveChannelService)
        {
            _receiveChannelService = receiveChannelService;
        }

        /// <summary>
        /// 开始接收画面
        /// </summary>
        /// <param name="id">场景标识</param>
        /// <param name="receptor">画面类型</param>
        /// <returns>接收标识</returns>
        [HttpPut]
        public object StartReceiveChannel(string id, string receptor)
        {
            return new 
            {
                ReceptionId = _receiveChannelService.StartReceiveChannel(id, receptor)
            };
        }

        /// <summary>
        /// 获取画面
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost]
        public List<ChannelInfo> ReceiveChanel(ReceiveChannelParam parameter)
        {
            return _receiveChannelService.ReceiveChannel(parameter);
        }

        /// <summary>
        /// 停止接收画面
        /// </summary>
        /// <param name="id">接收标识</param>
        /// <param name="receptor">画面类型</param>
        [HttpDelete]
        public void StopReceiveChannel(string id, string receptor)
        {
            _receiveChannelService.StopReceiveChannel(id, receptor);
        }
    }
}
