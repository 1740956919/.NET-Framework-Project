using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Interface
{
    public interface IVoteService
    {
        /// <summary>
        /// 开始投票
        /// </summary>
        /// <param name="brainstormingId">头脑风暴标识</param>
        /// <returns></returns>
        String StartVote(string brainstormingId);
        /// <summary>
        /// 结束投票
        /// </summary>
        /// <param name="voteId">投票标识</param>
        void StopVote(string voteId);
        /// <summary>
        /// 检查是否存在投票
        /// </summary>
        /// <param name="brainstormingId">头脑风暴标识</param>
        /// <returns></returns>
        VoteStateInfo GetVote(string brainstormingId);
        /// <summary>
        /// 获取投票内容(手机)
        /// </summary>
        /// <param name="voteId">头脑风暴标识</param>
        /// <returns></returns>
        MyVoteStateInfo GetMyVote(string brainstormingId);
        /// <summary>
        /// 递交投票结果(手机)
        /// </summary>
        /// <param name="voteId">投票标识</param>
        /// <param name="voteOptionIds">投票项标识列表</param>
        void SubmitMyVote(string voteId, List<String> voteOptionIds);

    }
}