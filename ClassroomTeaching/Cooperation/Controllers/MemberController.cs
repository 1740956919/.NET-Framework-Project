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
    /// 组成员管理控制器
    /// </summary>
    [AvatarAuthorize]
    public class MemberController : ApiController
    {
        private readonly ICooperationService _cooperationService = null;

        public MemberController(ICooperationService cooperationService)
        {
            _cooperationService = cooperationService;
        }

        /// <summary>
        /// 获取所有的组成员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public DiscussionMemberInfo Get(String id)
        {
            return _cooperationService.GetGroupMembers(id);
        }

        /// <summary>
        /// 更换主持人
        /// </summary>
        /// <param name="id"></param>
        /// <param name="receptor"></param>
        [HttpPut]
        public void ChangeChair(String id, String receptor)
        {
            _cooperationService.ChangeChair(id, receptor);
        }

        /// <summary>
        /// 主持讨论
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parameter"></param>
        [HttpPost]
        public void ChairDiscussion(String id, [FromBody]DiscussionChairParam parameter)
        {
            _cooperationService.ChairDiscussion(id, parameter);
        }
    }
}
