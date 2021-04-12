using LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Interface
{
    /// <summary>
    /// 分组安排服务
    /// </summary>
    public interface IGroupingService
    {
        /// <summary>
        /// 开始分组
        /// </summary>
        /// <param name="lessonId"></param>
        void Start(String lessonId);
        /// <summary>
        /// 停止分组
        /// </summary>
        /// <param name="lessonId"></param>
        void Stop(String lessonId);
        /// <summary>
        /// 获取分组结果
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        GroupResult GetGroupResult(String lessonId);
        /// <summary>
        /// 获取可选择的分组信息
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        OptionalGroupInfo GetOptionalGroupInfo(String lessonId);
        /// <summary>
        /// 加入分组
        /// </summary>
        /// <param name="lessonId"></param>
        /// <param name="groupId"></param>
        void Join(String lessonId, String groupId);        
    }
}
