using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Interface
{
    public interface IInteractService
    {
        /// <summary>
        /// 查询当前的上课状态
        /// </summary>
        /// <param name="lessonId">课时标识</param>
        /// <returns></returns>
        ClassStateResult GetClassState(string lessonId);
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
        /// <summary>
        /// 学生加入练习
        /// </summary>
        /// <param name="practiceId">练习标识</param>
        /// <returns>题型</returns>
        PracticeJoinResult JoinPractice(string practiceId);
        /// <summary>
        /// 学生提交练习答案
        /// </summary>
        /// <param name="practiceId">练习标识</param>
        /// <param name="answer">答案</param>
        void SubmitAnswer(string practiceId, string answer);
        /// <summary>
        /// 获取所有的投屏通道
        /// </summary>
        /// <param name="lessonId">课时标识</param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        List<ChannelInfo> GetAllChannels(string lessonId, string deviceType);
    }
}