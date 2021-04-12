using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Interface
{
    /// <summary>
    /// 提供关于抢答的能力
    /// </summary>
    public interface IGrabAnswerService
    {
        /// <summary>
        /// 创建抢答
        /// </summary>
        /// <param name="lessonId">课时标识</param>
        /// <returns>抢答标识</returns>
        string CreateGrabAnswer(string lessonId);
        /// <summary>
        /// 结束抢答
        /// </summary>
        /// <param name="grabAnswerId">抢答标识</param>
        void CompleteGrabAnswer(string grabAnswerId);
        /// <summary>
        /// 获取抢答结果信息
        /// </summary>
        /// <param name="grabAnswerId">抢答标识</param>        
        /// <returns></returns>
        GrabAnswerResult GetGrabAnswer(string grabAnswerId);
        /// <summary>
        ///学生端显示抢答信息
        /// </summary>
        /// <param name="grabAnswerId">抢答标识</param>
        /// <returns></returns>
        GrabAnswerJoinResult ShowGrabAnswer(string grabAnswerId);
        /// <summary>
        /// 学生参与抢答
        /// </summary>
        /// <param name="grabAnswerId">抢答标识</param>
        /// <returns></returns>
        GrabResult JoinGrabAnswer(string grabAnswerId);
    }
}