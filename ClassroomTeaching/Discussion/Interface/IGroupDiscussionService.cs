using LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Interface
{
    public interface IGroupDiscussionService
    {
        /// <summary>
        /// 获取分组讨论状态
        /// </summary>
        /// <param name="lessonId">课时标识</param>
        /// <returns></returns>
        DiscussionStateInfo GetGroupDiscussionState(string lessonId);
        /// <summary>
        /// 开始分组讨论
        /// </summary>
        /// <param name="lessonId">课时标识</param>
        /// <returns></returns>
        string BeginGroupDiscussion(string lessonId,string discussionType);
        /// <summary>
        /// 停止分组讨论
        /// </summary>
        /// <param name="discussionId">讨论标识</param>
        void StopGroupDiscussion(string discussionId);
        /// <summary>
        /// 结束分组讨论
        /// </summary>
        /// <param name="lessonId">课时标识</param>
        /// <param name="discussionId">讨论标识</param>
        void EndGroupDiscussion(string lessonId, string discussionId);
        /// <summary>
        /// 获取小组的监控情况
        /// </summary>
        /// <param name="discussionId">讨论标识</param>
        /// <returns></returns>
        DiscussionMonitorResult GetGroupDiscussionMonitor(string discussionId);
        /// <summary>
        /// 评审小组讨论结果
        /// </summary>
        /// <param name="discussionId">讨论标识</param>
        /// <returns></returns>
        DiscussionReviewResult GetGroupDiscussionReview(string discussionId);
        /// <summary>
        /// 参与小组讨论
        /// </summary>
        /// <param name="lessonId">课时标识</param>
        /// <param name="discussionId">进行中的讨论标识</param>
        /// <returns></returns>
        DiscussionJoinResult JoinDiscussion(string lessonId,string discussionId);
        /// <summary>
        /// 获取我当前正在参与的分组讨论（手机）
        /// </summary>
        /// <param name="groupDiscussionId">分组讨论标识</param>
        /// <returns></returns>
        MyDiscussionJoinResult GetMyDiscussion(string groupDiscussionId);
    }
}