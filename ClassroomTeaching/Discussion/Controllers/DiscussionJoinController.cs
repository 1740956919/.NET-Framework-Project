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
    public class DiscussionJoinController : ApiController
    {
        private readonly IGroupDiscussionService _groupDiscussionService = null;

        public DiscussionJoinController(IGroupDiscussionService groupDiscussionService)
        {
            _groupDiscussionService = groupDiscussionService;
        }

        /// <summary>
        /// 获取我当前正在参与的分组讨论
        /// </summary>
        /// <param name="id">讨论标识</param>
        /// <returns></returns>
        [HttpGet]
        public MyDiscussionJoinResult GetMyDiscussion(string id)
        {
            return _groupDiscussionService.GetMyDiscussion(id);
        }
    }
}
