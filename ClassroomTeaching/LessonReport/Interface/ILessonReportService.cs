using LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Struct;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Interface
{
    /// <summary>
    /// 提供导出课堂报告的能力
    /// </summary>
    public interface ILessonReportService
    {
        /// <summary>
        /// 查询课堂报告导出状态
        /// </summary>
        /// <param name="sceneId">场景标识</param>
        /// <returns></returns>
        LessonReportExportState GetLessonReportExportState(String sceneId);

        /// <summary>
        /// 查询课时
        /// </summary>
        /// <param name="sceneId">场景标识</param>
        /// <param name="parametor"></param>
        /// <returns></returns> 
        LessonQueryResult QueryLessons(String sceneId, LessonRecordQueryParam parametor);

        /// <summary>
        /// 查询课时
        /// </summary>
        /// <param name="sceneId">课时标识</param>
        /// <returns></returns> 
        List<LessonReportResult> GetLessonReportById(String lessonId);

        /// <summary>
        /// 导出课堂报告
        /// </summary>
        /// <param name="sceneId">场景标识</param> 
        /// <param name="parametor"></param>
        /// <returns></returns>     
        LessonReportExportResult ExportLessonReport(String sceneId, LessonReportExportInfo parametor);

        /// <summary>
        /// 取消导出
        /// </summary>
        /// <param name="behaviorId"></param>
        void CancelExport(String behaviorId);

        /// <summary>
        /// 添加课堂记录
        /// </summary>
        /// <param name="lessonId">课时标识</param>
        /// <param name="parameter"></param>
        void AddLessonRecord(string lessonId, LessonReportAddParam parameter);
    }
}