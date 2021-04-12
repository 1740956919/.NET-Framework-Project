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
    public class ReviewController : ApiController
    {
        private readonly IGroupDiscussionService _groupDiscussionService = null;

        public ReviewController(IGroupDiscussionService groupDiscussionService)
        {
            _groupDiscussionService = groupDiscussionService;
        }
         
        /// <summary>
        /// 评审小组讨论结果
        /// </summary>
        /// <param name="id">讨论标识</param>
        /// <returns></returns>
        [HttpGet]
        public DiscussionReviewResult GetGroupDiscussionReview(string id)
        {
            return _groupDiscussionService.GetGroupDiscussionReview(id);
        }
    }
}
