using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Interface
{
    public interface ILessonService
    {
        /// <summary>
        /// 获取课时内容
        /// </summary>
        /// <param name="lessonId">课时标识</param>
        /// <returns></returns>
        LessonContentResult GetLessonContent(String lessonId, String token);
        /// <summary>
        /// 下载资料
        /// </summary>
        /// <param name="materialId"></param>
        MaterialDownloadResult DownLoadMaterial(String materialId);
        /// <summary>
        /// 查看上课记录详情
        /// </summary>
        /// <param name="lessonId">课时标识</param>
        /// <returns></returns>
        List<ReportContentResult> QueryReportDetail(String lessonId);
        /// <summary>
        /// 验证我的互评打分是否完成
        /// </summary>
        /// <param name="ScoreSessonId"></param>
        /// <returns></returns>
        VerifyResult VerifyMyMutualScore(String ScoreSessonId);
        /// <summary>
        /// 学生打分
        /// </summary>
        /// <param name="ScoreSessonId"></param>
        /// <param name="score"></param>
        void Score(String ScoreSessonId, float score);
    }
}