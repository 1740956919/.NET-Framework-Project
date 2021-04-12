using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class VoteController : ApiController
    {
        private readonly IVoteService _voteService = null;

        public VoteController(IVoteService voteService)
        {
            _voteService = voteService;
        }

        /// <summary>
        /// 开始投票
        /// </summary>
        /// <param name="id">头脑风暴标识</param>
        /// <returns></returns>
        [HttpPut]
        public object StartVote(string id)
        {
            return new { VoteId = _voteService.StartVote(id) };
        }

        /// <summary>
        /// 结束投票
        /// </summary>
        /// <param name="id">投票标识</param>
        [HttpDelete]
        public void StopVote(string id)
        {
            _voteService.StopVote(id);
        }

        /// <summary>
        /// 检查是否存在投票
        /// </summary>
        /// <param name="id">头脑风暴标识</param>
        /// <returns></returns>
        [HttpGet]
        public VoteStateInfo GetVoteState(string id)
        {
            return _voteService.GetVote(id);
        }
    }
}
