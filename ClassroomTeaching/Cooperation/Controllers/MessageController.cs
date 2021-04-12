using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Controllers
{
    /// <summary>
    /// 消息管理控制器
    /// </summary>
    [AvatarAuthorize]
    public class MessageController : ApiController
    {
        private readonly ICooperationService _cooperationService = null;

        public MessageController(ICooperationService cooperationService)
        {
            _cooperationService = cooperationService;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPut]
        public MessageInfo Send(String id, MessageSendParam parameter)
        {
            return _cooperationService.SendMessage(id, parameter);
        }
    }
}
