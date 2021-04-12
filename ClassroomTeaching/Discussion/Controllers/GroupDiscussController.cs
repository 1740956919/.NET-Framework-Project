using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class GroupDiscussController : ApiController
    {
        private readonly IGroupDiscussionService _groupDiscussionService = null;

        public GroupDiscussController(IGroupDiscussionService groupDiscussionService)
        {
            _groupDiscussionService = groupDiscussionService;
        }

        /// <summary>
        /// 参与小组讨论
        /// </summary>
        /// <param name="id">课时标识</param>
        /// <param name="receptor">进行中的讨论标识</param>
        /// <returns></returns>
        [HttpGet]
        public DiscussionJoinResult DiscussionJoin(string id,string receptor)
        {
            return _groupDiscussionService.JoinDiscussion(id,receptor);
        }
    }
}
