using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Struct;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Interface
{
    /// <summary>
    /// 提供关于随机选人的能力
    /// </summary>
    public interface IRandomCandidateService
    {
        /// <summary>
        /// 创建随机选人
        /// </summary>
        /// <param name="sceneId">场景标识</param>
        /// <returns>随机选人标识</returns>
        string CreateRandomCandidate(string sceneId);
        /// <summary>
        /// 结束随机选人
        /// </summary>
        /// <param name="randomRollCallId">随机选人标识</param>
        /// <param name="sceneId">场景标识</param>
        void CompleteRandomCandidate(string randomRollCallId, string lessonId);
        /// <summary>
        /// 获取随机选人的结果
        /// </summary>
        /// <param name="randomRollCallId">随机选人标识</param>
        /// <returns></returns>
        RandomCandidateResult GetRandomCandidate(string randomRollCallId);
        /// <summary>
        /// 获取该场景下所有的学生
        /// </summary>
        /// <param name="sceneId"></param>
        /// <returns></returns>
        List<StudentInfo> GetAllStudentsBySceneId(string sceneId);
    }
}