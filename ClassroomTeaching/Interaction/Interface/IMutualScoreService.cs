using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct;
using System;


namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Interface
{
    /// <summary>
    /// 互评管理接口
    /// </summary>
    public interface IMutualScoreService
    {
        /// <summary>
        /// 开始互评
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        MutualScoreStartResult Start(MutualScoreStartParam parameter);
        /// <summary>
        /// 结束互评
        /// </summary>
        /// <param name="scoreSessonId"></param>
        void Stop(String scoreSessonId);
        /// <summary>
        /// 获取互评进度
        /// </summary>
        /// <param name="scoreSessonId"></param>
        /// <returns></returns>
        MutualScoreProgress GetProgress(String scoreSessonId);
        /// <summary>
        /// 获取互评信息
        /// </summary>
        /// <param name="classroomId"></param>
        /// <returns></returns>
        StudentMutualScoreInfo GetMutualScoreInfo(String classroomId);
        /// <summary>
        /// 验证我的互评打分是否完成
        /// </summary>
        /// <param name="scoreSessonId"></param>
        /// <returns></returns>
        StudentVerifyResult VerifyMyMutualScore(String scoreSessonId);
        /// <summary>
        /// 学生打分
        /// </summary>
        /// <param name="scoreSessonId"></param>
        /// <param name="score"></param>
        void Score(String scoreSessonId, float score);
    }
}
