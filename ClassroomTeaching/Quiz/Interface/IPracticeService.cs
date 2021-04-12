using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Interface
{
    /// <summary>
    /// 提供关于练习的能力
    /// </summary>
    public interface IPracticeService
    {
        /// <summary>
        /// 创建练习
        /// </summary>
        /// <param name="parameter">练习创建参数</param>
        /// <returns>练习标识</returns>
        String CreatePractice(PracticeCreateParam parameter);
        /// <summary>
        /// 停止练习
        /// </summary>
        /// <param name="practiceId">练习标识</param>
        void StopPractice(string practiceId);
        /// <summary>
        /// 结束练习
        /// </summary>
        /// <param name="practiceId">练习标识</param>
        /// <param name="sceneId">课时标识</param>
        void CompletePractice(string practiceId, string lessonId);

        /// <summary>
        /// 获取练习状态
        /// </summary>
        /// <param name="practiceId">练习标识</param>
        /// <returns>练习状态</returns>
        String GetPracticeState(string practiceId);
        /// <summary>
        /// 获取练习答题进度信息
        /// </summary>
        /// <param name="practiceId">练习标识</param>
        /// <returns></returns>
        PracticeProgressResult GetPracticeProgress(string practiceId);
        /// <summary>
        /// 获取停止练习后的答题信息
        /// </summary>
        /// <param name="practiceId">练习标识</param>
        /// <returns></returns>
        PracticeReviewResult GetPracticeInfo(string practiceId);
     
        /// <summary>
        /// 显示学生提交答案的简报信息
        /// </summary>
        /// <param name="practiceId">练习标识</param>
        /// <returns></returns>
        List<PracticeBriefResult> GetPracticeBrief(string practiceId);
        /// <summary>
        /// 显示学生提交答案的详情信息
        /// </summary>
        /// <param name="practiceId">练习标识</param>
        /// <param name="sceneId">场景标识</param>
        /// <returns></returns>
        PracticeDetailResult GetPracticeDetail(string practiceId, string sceneId);
        /// <summary>
        /// 给学生打分
        /// </summary>
        /// <param name="practiceId">练习标识</param>
        /// <param name="studentId">学生标识</param>
        /// <param name="Score">得分</param>
        void GradeStudent(string practiceId, string studentId, float Score);
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
    }
}