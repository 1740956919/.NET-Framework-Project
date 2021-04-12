using LINDGE.PARA.Generic.Behavior.Single.API.Basic;
using LINDGE.PARA.Generic.Behavior.Single.API.Query;
using LINDGE.PARA.Generic.Behavior.Single.Const;
using LINDGE.PARA.Generic.Behavior.Single.Param;
using LINDGE.PARA.Generic.Behavior.Single.Query;
using LINDGE.PARA.Generic.Behavior.Single.Struct;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Const;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Param;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Service.Interface;
using LINDGE.PARA.Query.Base.Param;
using LINDGE.PARA.Runtime.BehaviorJob;
using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Struct;
using LINDGE.PARA.Translayer.Static.Picture.WebAPI;
using LINDGE.PARA.Translayer.Static.Picture.WebAPI.Param;
using LINDGE.PARA.Translayer.Static.Picture.WebAPI.Struct;
using LINDGE.Proxy;
using LINDGE.Serialization;

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Service
{
    public class LessonReportService : ILessonReportService
    {
        private readonly ILesson _lesson = null;
        private readonly IIOProvider _ioProvider = null;
        private readonly ILessonRecord _lessonRecord = null;
        private readonly IConfigSource _configSource = null;
        private readonly IBehaviorInfo _behaviorInfo = null;
        private readonly IBatchBehavior _batchBehavior = null;
        private readonly IBehaviorQuery _behaviorQuery = null;
        private readonly IBehaviorExecution _behaviorExecution = null;
        private readonly IBehaviorAttribute _behaviorAttribute = null;
        private readonly IBehaviorCompletion _behaviorCompletion = null;
        private readonly IPictureUpload _pictureUpload = null;

        public LessonReportService(
            ILesson lessonService,
            IIOProvider ioProviderService,
            ILessonRecord lessonRecordService,
            IConfigSource configSourceService,
            IProxy<IBehaviorInfo> behaviorInfoProxy,
            IProxy<IBatchBehavior> batchBehaviorProxy,
            IProxy<IBehaviorQuery> behaviorQueryProxy,
            IProxy<IBehaviorExecution> behaviorExecutionProxy,
            IProxy<IBehaviorAttribute> behaviorAttributeProxy,
            IProxy<IBehaviorCompletion> behaviorCompletionProxy,
            IProxy<IPictureUpload> pictureUploadProxy)
        {
            _lesson = lessonService;
            _ioProvider = ioProviderService;
            _lessonRecord = lessonRecordService;
            _configSource = configSourceService;
            _behaviorInfo = behaviorInfoProxy.GetObject();
            _batchBehavior = batchBehaviorProxy.GetObject();
            _behaviorQuery = behaviorQueryProxy.GetObject();
            _behaviorExecution = behaviorExecutionProxy.GetObject();
            _behaviorAttribute = behaviorAttributeProxy.GetObject();
            _behaviorCompletion = behaviorCompletionProxy.GetObject();
            _pictureUpload = pictureUploadProxy.GetObject();
        }

        /// <summary>
        /// 查询课时
        /// </summary>
        /// <param name="sceneId">场景标识</param>
        /// <param name="parametor"></param>
        /// <returns></returns> 
        public LessonQueryResult QueryLessons(string sceneId, LessonRecordQueryParam parametor)
        {
            var lessonQueryResult = new LessonQueryResult();
            var queryLessonResult = _lesson.QueryLesson(new LessonQueryParam()
            {
                SceneId = sceneId,
                IsUsePage = true,
                Page = new PageParam()
                {
                    PageIndex = parametor.PageIndex,
                    PageSize = parametor.PageSize,
                    Order = parametor.OrderName,
                },
                IsIncludeState = true,
                States = new List<string>() { LessonState.Stop }
            });
            lessonQueryResult.TotalCount = queryLessonResult.TotalCount;
            var lessonIds = queryLessonResult.LessonInfos.Select(l => l.LessonId).ToList();
            if (lessonIds.Count > 0)
            {
                var teacherName = _configSource.GetSection<LessonReportConfigSection>(LessonReportConfigSection.DefaultSectionName).TemporaryTeacherName;
                var lessonRecordInfos = _lesson.GetLessonOverview(lessonIds);
                var lessonRecordMap = lessonRecordInfos.ToDictionary(l => l.LessonId);
                
                lessonQueryResult.LessonInfos = lessonIds.Select(l => new LessonInfo()
                {
                    LessonId = lessonRecordMap[l].LessonId,
                    LessonName = lessonRecordMap[l].LessonName,                              
                    TeachingClassName = lessonRecordMap[l].TeachingClassName,
                    BeginTime = lessonRecordMap[l].BeginTime,
                    EndTime = lessonRecordMap[l].EndTime,
                    StudentCount = lessonRecordMap[l].JoinedStudentCount,          
                    TeacherName = String.IsNullOrEmpty(lessonRecordMap[l].TeacherName) ? teacherName : lessonRecordMap[l].TeacherName
                }).ToList();
            }
            return lessonQueryResult;
        }

        

        /// <summary>
        /// 导出课堂报告
        /// </summary>
        /// <param name="sceneId">场景标识</param>
        /// <param name="parametor"></param>
        /// <returns></returns>   
        public LessonReportExportResult ExportLessonReport(string sceneId, LessonReportExportInfo parametor)
        {
            var result = new LessonReportExportResult();
            //检查文件是否已经存在
            var filePath = _ioProvider.Combine(SchemaNames.FileStorage, RootNames.Export, sceneId, parametor.LessonId);
            if (_ioProvider.Exists(filePath))
            {
                var lessonResult = _lesson.Get(parametor.LessonId);
                if(lessonResult != null)
                {
                    var startTime = lessonResult.BeginTime.ToLocalTime();
                    var beginDate = startTime.ToShortDateString().ToString();
                    var beginTime = startTime.ToShortTimeString().ToString();
                    var beginDateTime = $"{beginDate}-{beginTime}";
                    beginDateTime = beginDateTime.Replace('/', '-').Replace(':', '-');
                    result.LessonReportName = $"{ lessonResult.LessonName }{ beginDateTime}";
                }                        
                result.IsCompleted = true;              
            }
            else
            {
                //创建导出行为
                result.BehaviorId = CreateLessonExportBehavior(sceneId, parametor.LessonId, parametor.DownloadDirectory);
            }
            return result;
        }

        /// <summary>
        /// 创建课堂报告导出行为
        /// </summary>
        /// <param name="sceneId">场景标识</param>
        /// <param name="lessonId">课时标识</param>
        /// <param name="DownloadDirectory">下载目录</param>
        /// <returns></returns>
        private string CreateLessonExportBehavior(String sceneId, String lessonId, String DownloadDirectory)
        {
            var savePath = _ioProvider.Combine(SchemaNames.ZipStorage, RootNames.Export, sceneId, lessonId);
            var behaviorId = String.Empty;
            //行为创建参数
            var behaviorCreateParam = new BehaviorCreateParam()
            {
                Action = BehaviorActionNames.ExportLessonReport,
                Exclusive = new BehaviorExclusive()
                {
                    ActionList = new List<String>() { BehaviorActionNames.ExportLessonReport },
                    ActionRange = ActionExclusiveRange.SingleAction,
                    PrincipalRange = PrincipalExclusiveRange.Global
                },
                Principal = new BehaviorPrincipal()
                {
                    Type = Generic.Behavior.Single.Const.PrincipalType.MySelf
                },
                Reception = new BehaviorReception()
                {
                    Type = ReceptionType.Identify,
                    Data = sceneId
                }
            };
            var behaviorCreateResult = _batchBehavior.Create(new List<BehaviorCreateParam>() { behaviorCreateParam });

            //行为是否创建成功
            if (behaviorCreateResult[0].Success)
            {
                behaviorId = behaviorCreateResult[0].BehaviorId;
                //课时信息
                var lessonResult = _lesson.Get(lessonId);
                var startTime =  lessonResult.BeginTime.ToLocalTime();
                var beginDate = startTime.ToShortDateString().ToString();
                var beginTime = startTime.ToShortTimeString().ToString();
                var beginDateTime = $"{beginDate}-{beginTime}";
                beginDateTime = beginDateTime.Replace('/', '-').Replace(':', '-');
               
                var lessonReportInfo = new LessonReportInfo()
                {
                    LessonId = lessonResult.LessonId,
                    LessonReportName = $"{ lessonResult.LessonName }{ beginDateTime}"
                };
                //将要导出课堂报告的课时信息存入行为属性中               
                var behaviorAttributeUpdateParam = new BehaviorAttributeUpdateParam()
                {
                    BehaviorIds = new List<string>() { behaviorId },
                    UpdateParams = new Dictionary<string, string>()
                    {
                        { AttributeNames.LessonReportInfo, lessonReportInfo.ToJSONString() },
                        { AttributeNames.DownloadDirectory, DownloadDirectory}
                    }
                };
                _behaviorAttribute.Update(new List<BehaviorAttributeUpdateParam>() { behaviorAttributeUpdateParam });

            
                #region 添加job步骤
                var jobBuilder = new JobBuilder(_configSource);
                jobBuilder.SetDescription("正在导出课堂报告");
                jobBuilder.SetWorkLane(String.Empty);

                //课堂记录信息
                var lessonReportExportInfo = _lessonRecord.ExportLessonRecord(lessonId);
                if(lessonReportExportInfo != null)
                {
                    ExportLessonRecord(savePath, lessonReportExportInfo.ToJSONString());
                    if (lessonReportExportInfo.ActivityRecordInfo.Count > 0)
                    {
                        var activityPictureIds = lessonReportExportInfo.ActivityRecordInfo.Where(a => !String.IsNullOrEmpty(a.PictureId))
                            .Select(a => a.PictureId).Distinct().ToList();
                        if (activityPictureIds.Count > 0)
                        {
                            var pageIndex = 0;
                            var pageSize = 5;
                            //添加步骤
                            do
                            {
                                var pictureIds = activityPictureIds.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                                jobBuilder.AddTryWebAPIStep<IExportActivityPicture>($"正在导出活动图片", 0, "|Translayer.ClassroomTeaching.Callback|", "ExportPictures", new PictureExportParam()
                                {
                                    PictureIds = pictureIds,
                                    LessonReportFilePath = savePath
                                });
                                pageIndex++;
                            } while (pageIndex * pageSize < activityPictureIds.Count);
                        }
                    }
                }
                jobBuilder.AddFinallyWebAPIStep<IBehaviorCompletion>($"结束行为", 0, "|Generic.Behavior.Single|", "Complete", new List<BehaviorCompleteParam>()
                {
                    new BehaviorCompleteParam()
                    {
                        BehaviorIds = new List<string>() { behaviorId },
                        ResultCode = "CompleteExport",
                        ResultType = ResultType.UserDefine
                    }
                });
                #endregion
                _behaviorExecution.Execute(new List<BehaviorExecuteParam>()
                {
                    new BehaviorExecuteParam()
                    {
                        Action = BehaviorActionNames.ExportLessonReport,
                        BehaviorIds = new List<String>(){ behaviorId },
                        JobParameters = new List<Engine.Job.JobParameter>()
                        {
                            jobBuilder.GetJobParameter()
                        }
                    }
                });           
            }
            else if (!String.IsNullOrWhiteSpace(behaviorCreateResult[0].OccupiedBehaviorId))
            {
                behaviorId = behaviorCreateResult[0].OccupiedBehaviorId;
            }
            return behaviorId;
        }

        /// <summary>
        /// 查询课堂报告导出状态
        /// </summary>
        /// <param name="sceneId">场景标识</param>
        /// <returns></returns>
        public LessonReportExportState GetLessonReportExportState(string sceneId)
        {
            var result = new LessonReportExportState();

            var operatorExpression = new List<IConditionParameter>
            {
                new EqualOperator() { Name = QueryName.Action, Value = BehaviorActionNames.ExportLessonReport },
                new EqualOperator() { Name = QueryName.ReceptionData, Value = sceneId },
                new EqualOperator() { Name = QueryName.IsComplete, Value = "FALSE" }
            };
            var behaviorQueryResults = _behaviorQuery.ComplexQuery(new List<QueryParameter>()
            {
                new QueryParameter()
                {
                    Conditions = new AndOperator()
                    {
                        OperatorExpression = new List<IConditionParameter>()
                        {
                            new AndOperator()
                            {
                                OperatorExpression = operatorExpression
                            }
                        }
                    },
                    Order = new List<OrderParameter>() { 
                        new OrderParameter()
                        {
                            Name = Generic.Behavior.Single.Query.OrderName.LastExecuteTime, 
                            IsDesc = true } 
                        },
                    Page = new PageParameter() { PageIndex = 0, PageSize = 1 }
                }
            }).ToList();

            if (behaviorQueryResults[0] != null && behaviorQueryResults[0].Results != null && behaviorQueryResults[0].Results.Count > 0)
            {
                result.BehaviorId = behaviorQueryResults[0].Results[0];
                result.State = LessonExportState.Progressing;
            }
            else
            {
                result.State = LessonExportState.UnStart;
            }

            return result;
        }

        /// <summary>
        /// 取消导出
        /// </summary>
        /// <param name="behaviorId"></param>
        public void CancelExport(string behaviorId)
        {
            var behaviorInfos = _behaviorInfo.GetBehaviorInfo(new List<BehaviorInfoGetParam>()
            {
                new BehaviorInfoGetParam()
                {
                    BehaviorIds = new List<string>(){ behaviorId },
                    IsIncludeState = true
                }
            });

            if (behaviorInfos != null && behaviorInfos.Count > 0 && behaviorInfos[0] != null)
            {
                var behaviorState = behaviorInfos[0].State;
                if (behaviorState.HasFlag(BehaviorState.IsExecutable))
                {
                    _behaviorCompletion.Complete(new List<BehaviorCompleteParam>()
                    {
                        new BehaviorCompleteParam()
                        {
                            BehaviorIds = new List<string>(){ behaviorId },
                            ResultCode = "Delete",
                            ResultType = ResultType.UserDefine
                        }
                    });
                }
            }
        }

        public void AddLessonRecord(string lessonId, LessonReportAddParam parameter)
        {
            var pictureId = string.Empty;
            // 将上传目录下的图片转存到Static下
            if (!string.IsNullOrWhiteSpace(parameter.Picture) && _ioProvider.Exists(parameter.Picture))
            {
                var pictureSpec = _configSource.GetSection<LessonReportConfigSection>(LessonReportConfigSection.DefaultSectionName).PictureSpec;
                var uploadResult = _pictureUpload.Upload(new PictureUploadParam()
                {
                    Pictures = new List<FileData>()
                    {
                        new FileData()
                        {
                            Location = parameter.Picture
                        }
                    },
                    Specs = new List<PictureSpec>() { pictureSpec }
                })[0];
                pictureId = uploadResult.IsSuccess ? uploadResult.PictureId : string.Empty;
            }            

            _lessonRecord.Add(new Generic.ClassroomTeaching.Scene.Param.LessonRecordAddParam()
            {
                Action = parameter.Action,
                Content = parameter.Content,
                LessonId = lessonId,
                Picture = pictureId
            });
        }

        /// <summary>
        /// 获取课堂记录数据
        /// </summary>
        /// <param name="lessonId">课时标识</param>
        /// <returns></returns>
        public List<LessonReportResult> GetLessonReportById(string lessonId)
        {
            var result = new List<LessonReportResult>();
            var lessonReportInfos = _lessonRecord.GetTeacherActivityRecord(lessonId);          
            if(lessonReportInfos.Count > 0)
            {
                result = lessonReportInfos.OrderBy(l => l.OccurredTime).Select(a => new LessonReportResult()
                { 
                    RecordTime = a.OccurredTime,
                    Title = a.ActivityTitle,
                    Category = a.IconName,
                    Image = a.PictureId,
                    ContentType = a.Detail?.DetailType,
                    Content = a.Detail?.DetailData,
                    RelateData = new RelateData()
                    {
                        IsRelated = !String.IsNullOrEmpty(a.Relevance?.RelevanceId),
                        RelateId = a.Relevance?.RelevanceId,
                        Title = a.Relevance?.RelevanceTitle,
                        RelateType = a.Relevance?.RelevanceType
                    }
                }).ToList();
            }
            return result;
        }

        /// <summary>
        /// 生成JSON文件
        /// </summary>
        /// <param name="parametor"></param>
        public void ExportLessonRecord(string savePath, string lessonRecordInfo)
        {
            var zipFilePath = savePath.Replace(SchemaNames.ZipStorage, SchemaNames.FileStorage);
            using (var zipFileStream = _ioProvider.Open(zipFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using (var zipArchive = new ZipArchive(zipFileStream, ZipArchiveMode.Update))
            {
                Byte[] charArray = Encoding.UTF8.GetBytes(lessonRecordInfo);
                MemoryStream memoryStream = new MemoryStream(charArray);
                var entry = zipArchive.CreateEntry("LessonRecord.json");
                var entryStream = entry.Open();
                memoryStream.CopyTo(entryStream);
                memoryStream.Close();
            }
        }
    }
}