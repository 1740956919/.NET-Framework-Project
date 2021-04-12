using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Struct;
using LINDGE.PARA.Generic.ClassroomTeaching.Brainstorming.Service.Interface;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Service
{
    public class VoteService : IVoteService
    {
        private readonly ICategory _category = null;
        private readonly ICategoryVote _categoryVote = null;
        private readonly ITimeProvider _timeProvider = null;

        public VoteService(
            ICategory categoryService,
            ICategoryVote categoryVoteService,
            ITimeProvider timeProviderService
            )
        {
            _category = categoryService;
            _categoryVote = categoryVoteService;
            _timeProvider = timeProviderService;
        }
        /// <summary>
        /// 获取投票内容(手机)
        /// </summary>
        /// <param name="voteId">头脑风暴标识</param>
        /// <returns></returns>
        public MyVoteStateInfo GetMyVote(string brainstormingId)
        {
            var myVoteStateInfo = new MyVoteStateInfo();
            var voteStateInfo = _categoryVote.ExistVote(brainstormingId);
            //存在投票
            if (voteStateInfo.IsVoting)
            {
                myVoteStateInfo.CanVote = true;
                myVoteStateInfo.VoteInfo.VoteId = voteStateInfo.VoteId;
                var voteId = voteStateInfo.VoteId;
                
                //我的投票结果
                var myVoteResult = _categoryVote.MyVoteResult(voteId);
                myVoteStateInfo.VoteInfo.IsSubmitted = myVoteResult.Any();

                //投票内容
                var voteOptionInfos = _categoryVote.GetVoteOption(voteId);
                myVoteStateInfo.VoteInfo.VoteOpitions = voteOptionInfos.Select(v => new VoteOptionInfo()
                {
                    Id = v.CategoryId,
                    Name = v.Name
                }).ToList();
            }
            return myVoteStateInfo;
        }

        /// <summary>
        /// 检查是否存在投票
        /// </summary>
        /// <param name="brainstormingId">头脑风暴标识</param>
        /// <returns></returns>
        public VoteStateInfo GetVote(string brainstormingId)
        {
            var result = new VoteStateInfo();
            var voteStateInfo = _categoryVote.ExistVote(brainstormingId);
            result.IsVoting = voteStateInfo.IsVoting;
            if (voteStateInfo.IsVoting)
            {
                result.VoteId = voteStateInfo.VoteId;
                var nowTime = _timeProvider.GetNow();
                var startTime = (DateTime)voteStateInfo.StartTime;
                var duringSeconds = Convert.ToInt32((nowTime.ToUniversalTime().Subtract(startTime.ToUniversalTime())).TotalSeconds);

                //所有分类信息
                var categoryInfos = _category.GetAllCategory(brainstormingId);
                var categoryIds = categoryInfos.Select(c => c.CategoryId).ToList();
                //分类投票结果
                var categroyVoteInfos = _categoryVote.GetVoteResult(brainstormingId, categoryIds);
                
                if(categroyVoteInfos != null && categroyVoteInfos.VoteInfos.Count > 0)
                {
                    result.VoteInfo = new VoteInfo()
                    {
                        DuringSeconds = duringSeconds,
                        JoinCount = categroyVoteInfos.VoterCount,
                        VoteResult = categroyVoteInfos.VoteInfos.Select(c => new VoteResult()
                        {
                            CategoryId = c.CategoryId,
                            VoteCount = c.VoteCount,
                            IsVoted = c.IsVoted
                        }).ToList()
                    };
                }             
            }
          
            return result;
        }

        /// <summary>
        /// 开始投票
        /// </summary>
        /// <param name="brainstormingId">头脑风暴标识</param>
        /// <returns></returns>
        public string StartVote(string brainstormingId)
        {
            return _categoryVote.StartVote(brainstormingId);
        }

        /// <summary>
        /// 结束投票
        /// </summary>
        /// <param name="voteId">投票标识</param>
        public void StopVote(string voteId)
        {
            _categoryVote.StopVote(voteId);
        }

        /// <summary>
        /// 递交投票结果(手机)
        /// </summary>
        /// <param name="voteId">投票标识</param>
        /// <param name="voteOptionIds">投票项标识列表</param>
        public void SubmitMyVote(string voteId, List<String> voteOptionIds)
        {
            if (voteOptionIds.Count > 0)
            {
                _categoryVote.SubmitVote(voteId, voteOptionIds);
            }
            
        }
    }
}