using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class LifecycleController : ApiController
    {
        private readonly IGroupDiscussionService _groupDiscussionService = null;

        public LifecycleController(IGroupDiscussionService groupDiscussionService)
        {
            _groupDiscussionService = groupDiscussionService;
        }

        /// <summary>
        /// 获取分组讨论状态
        /// </summary>
        /// <param name="id">课时标识</param>
        /// <returns></returns>
        [HttpGet]
        public DiscussionStateInfo GetGroupDiscussionState(string id)
        {
            return _groupDiscussionService.GetGroupDiscussionState(id);
        }

        /// <summary>
        /// 开始分组讨论
        /// </summary>
        /// <param name="id">课时标识</param>
        /// <returns></returns>
        [HttpPut]
        public GroupDiscussionResult BeginGroupDiscussion(string id, [FromBody] BrainstormingCreateParam brainstormingCreateParam)
        {
            var discussionId = _groupDiscussionService.BeginGroupDiscussion(id, brainstormingCreateParam.DiscussionType);
            return new GroupDiscussionResult()
            {
                DiscussionId = discussionId
            };
        }

        /// <summary>
        /// 停止分组讨论
        /// </summary>
        /// <param name="id">讨论标识</param>
        [HttpPost]
        public void StopGroupDiscussion(string id)
        {
            _groupDiscussionService.StopGroupDiscussion(id);
        }

        /// <summary>
        /// 结束分组讨论
        /// </summary>
        /// <param name="id">课时标识</param>
        /// <param name="receptor">讨论标识</param>
        [HttpDelete]
        public void EndGroupDiscussion(string id, string receptor)
        {
            _groupDiscussionService.EndGroupDiscussion(id, receptor);
        }
    }
}
