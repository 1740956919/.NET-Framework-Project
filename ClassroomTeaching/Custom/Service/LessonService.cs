using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Const;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Service.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Struct;
using LINDGE.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Service
{
    public class LessonService : ILessonService
    {
        private readonly IEvaluative _evaluative = null;
        private readonly ITeachingMaterial _teachingMaterial = null;
        private readonly ILesson _lesson = null;
        private readonly IConfigSource _configSource = null;
        private readonly IFrontRouteProvider _frontRouteProvider = null;

        public LessonService(IEvaluative evaluative, 
            ITeachingMaterial teachingMaterial,
            ILesson lesson,
            IConfigSource configSource,
            IFrontRouteProvider frontRouteProvider)
        {
            _evaluative = evaluative;
            _teachingMaterial = teachingMaterial;
            _lesson = lesson;
            _configSource = configSource;
            _frontRouteProvider = frontRouteProvider;
        }

        /// <summary>
        /// 下载资料
        /// </summary>
        /// <param name="materialId"></param>
        public MaterialDownloadResult DownLoadMaterial(String materialId)
        {
            var result = new MaterialDownloadResult();
            result.MaterialStream = _teachingMaterial.DownloadMaterial(materialId);
            var materialInfos = _teachingMaterial.Get(new List<string>() { materialId });
            if (materialInfos.Any())
            {
                result.MaterialName = Path.GetFileName(materialInfos[0].Path);
            }

            return result;
        }

        /// <summary>
        /// 获取课时内容
        /// </summary>
        /// <param name="lessonId">课时标识</param>
        /// <returns></returns>
        public LessonContentResult GetLessonContent(String lessonId, String token)
        {
            var result = new LessonContentResult();
            var config = new FrontRouteSection();
            var urlMap = new Dictionary<String, String>();
            var url = String.Empty;

            // 获取场景信息
            var lessonInfo = _lesson.Get(lessonId);
            var section = _configSource.GetSection<CustomConfigSection>(CustomConfigSection.DefaultSectionName);
            result.LessonName = String.IsNullOrWhiteSpace(lessonInfo.LessonName) ? section.DefaultLessonName : lessonInfo.LessonName;
            result.CourseName = String.IsNullOrWhiteSpace(lessonInfo.TeachingClassName) ? section .DefaultClassName : lessonInfo.TeachingClassName;
            // 获取下发过的资料信息
            var openedMaterialGetResult = _teachingMaterial.GetOpenedMaterials(lessonId);
            var lessonMaterials = openedMaterialGetResult.Where(o => o.CatalogCode == CatalogCodes.LessonMaterial);
            if (lessonMaterials.Any())
            {
                config = _configSource.GetSection<FrontRouteSection>("FrontRoute");
                urlMap = config.RouteTables[0].EntranceTable.ToDictionary(e => e.Key, e => EnvironmentVariableConverter.Translate(e.Value));
                url = urlMap[section.MaterialPreviewEntranceName];
            }
            if (openedMaterialGetResult.Any())
            {
                result.Materials = openedMaterialGetResult.OrderBy(o => o.OpenTime).Select(o => new MaterialInfo()
                {
                    Id = o.Id,
                    Name = o.Name,
                    Type = o.Type,
                    CanPreview = o.CatalogCode == CatalogCodes.LessonMaterial ? true : false,
                    PreviewUrl = o.CatalogCode == CatalogCodes.LessonMaterial ? GetPreviewUrl(url, o.Id, lessonId, token) : String.Empty,
                    CanDownload = o.CatalogCode == CatalogCodes.LocalMaterial ? true : false
                }).ToList();
            }

            return result;
        }

        /// <summary>
        /// 查看上课记录详情
        /// </summary>
        /// <param name="lessonId">课时标识</param>
        /// <returns></returns>
        public List<ReportContentResult> QueryReportDetail(String lessonId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 学生打分
        /// </summary>
        /// <param name="ScoreSessonId">互评会话标识</param>
        /// <param name="score">Score</param>
        public void Score(String ScoreSessonId, float score)
        {
            _evaluative.Submit(ScoreSessonId, score);
        }

        /// <summary>
        /// 验证我的互评打分是否完成
        /// </summary>
        /// <param name="ScoreSessonId"></param>
        /// <returns></returns>
        public VerifyResult VerifyMyMutualScore(String ScoreSessonId)
        {
            var result = new VerifyResult();
            try
            {
                // 加入互评
                var joinResult = _evaluative.Join(ScoreSessonId);
                if (joinResult.IsScored)
                {
                    result.IsCompleted = true;
                }
            }
            catch (ErrorCodeException ex)
            {
                var resultCode = ExceptionHelper.GetCodeByException(ex);
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent((new { Code = resultCode }).ToJSONString())
                });
            }
            return result;
        }

        private String GetPreviewUrl(String url, String resourceId, String lessonId, String token)
        {
            return String.Format(url,resourceId, lessonId, token);
        }
    }
}