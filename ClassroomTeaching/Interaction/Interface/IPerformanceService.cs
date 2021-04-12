using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct;
using System;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Interface
{
    /// <summary>
    /// 课堂表现管理接口
    /// </summary>
    public interface IPerformanceService
    {
        /// <summary>
        /// 开始课堂表现评价
        /// </summary>
        /// <param name="classroomId"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        PerformanceScoreStartResult StartScore(String classroomId, ScoreStartParam parameter);
        /// <summary>
        /// 完成评价
        /// </summary>
        /// <param name="scoreBehaviorId"></param>
        /// <param name="parameter"></param>
        void CompleteScore(String scoreBehaviorId, ScoreCompleteParam parameter);
    }
}
