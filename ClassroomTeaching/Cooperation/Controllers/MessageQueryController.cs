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
    /// 消息查询控制器
    /// </summary>
    [AvatarAuthorize]
    public class MessageQueryController : ApiController
    {
        private readonly ICooperationService _cooperationService = null;

        public MessageQueryController(ICooperationService cooperationService)
        {
            _cooperationService = cooperationService;
        }

        /// <summary>
        /// 获取历史消息列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public HistoryMessageInfo Get(String id)
        {
            return _cooperationService.GetHistoryMessageInfo(id);
        }

        /// <summary>
        /// 查询指定时间后的消息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost]
        public List<MessageInfo> Query(String id, [FromBody]MessageQueryParam parameter)
        {
            return _cooperationService.QueryPeriodMessages(id, parameter);
        }
    }
}
