using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class VoteJoinController : ApiController
    {
        private readonly IVoteService _voteService = null;

        public VoteJoinController(IVoteService voteService)
        {
            _voteService = voteService;
        }

        /// <summary>
        /// 获取投票内容(手机)
        /// </summary>
        /// <param name="id">头脑风暴标识</param>
        /// <returns></returns>
        [HttpGet]
        public MyVoteStateInfo GetMyVote(string id)
        {
            return _voteService.GetMyVote(id);
        }

        /// <summary>
        /// 递交投票结果(手机)
        /// </summary>
        /// <param name="id">投票标识</param>
        /// <param name="voteParam"></param>
        [HttpPost]
        public void SubmitMyVote(string id, [FromBody] VoteParam voteParam)
        {
            _voteService.SubmitMyVote(id, voteParam.VoteOpitionIds);
        }
    }
}
