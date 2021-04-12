using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Interface;
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
    /// 讨论状态控制器
    /// </summary>
    [AvatarAuthorize]
    public class StateController : ApiController
    {
        private readonly ICooperationService _cooperationService = null;

        public StateController(ICooperationService cooperationService)
        {
            _cooperationService = cooperationService;
        }

        /// <summary>
        /// 获取协作讨论状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public CooperationStatus Get(String id)
        {
            return _cooperationService.GetCooperationStatus(id);
        }
    }
}
