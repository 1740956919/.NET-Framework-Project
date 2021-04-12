using LINDGE.PARA.Generic.Bank.Resource.Const;
using LINDGE.PARA.Generic.Bank.Resource.Param;
using LINDGE.PARA.Generic.Bank.Resource.Query;
using LINDGE.PARA.Generic.Bank.Resource.Struct;
using LINDGE.PARA.Generic.Bank.Resource.WebAPI.Basic;
using LINDGE.PARA.Generic.Bank.Resource.WebAPI.Query;
using LINDGE.PARA.Generic.Behavior.Single.API.Basic;
using LINDGE.PARA.Generic.Behavior.Single.Const;
using LINDGE.PARA.Generic.Behavior.Single.Param;
using LINDGE.PARA.Generic.Behavior.Single.Struct;
using LINDGE.PARA.Generic.ClassroomTeaching.Lesson.Service.Interface;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Const;
using LINDGE.PARA.Generic.TeachingSpace.WebAPI.Lesson;
using LINDGE.PARA.Query.Base.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct;
using LINDGE.Proxy;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Service
{
    public class MaterialService : IMaterialService
    {
        private readonly IClassroom _classroom = null;
        private readonly IIOProvider _iOProvider = null;
        private readonly IConfigSource _configSource = null;
        private readonly ITimeProvider _timeProvider = null;
        private readonly IResourceInfo _resourceInfo = null;
        private readonly IResourceUsage _resourceUsage = null;
        private readonly ILessonContent _lessonContent = null;
        private readonly IBatchBehavior _batchBehavior = null;
        private readonly IResourceQuery _resourceQuery = null;
        private readonly ILessonManagement _lessonManagement = null;
        private readonly IResource _resource = null;

        public MaterialService(IClassroom classroom,
            IIOProvider iOProvider,
            IConfigSource configSource,
            ITimeProvider timeProvider,
            IProxy<IResourceInfo> resourceInfoProxy,
            IProxy<IResourceUsage> resourceUsageProxy,
            IProxy<ILessonContent> lessonContentProxy,
            IProxy<IBatchBehavior> batchBehaviorProxy,
            IProxy<IResourceQuery> resourceQueryProxy,
            IProxy<ILessonManagement> lessonManagementProxy,
            IProxy<IResource> resourceProxy)
        {
            _classroom = classroom;
            _iOProvider = iOProvider;
            _configSource = configSource;
            _timeProvider = timeProvider;
            _resourceInfo = resourceInfoProxy.GetObject();
            _resourceUsage = resourceUsageProxy.GetObject();
            _lessonContent = lessonContentProxy.GetObject();
            _batchBehavior = batchBehaviorProxy.GetObject();
            _resourceQuery = resourceQueryProxy.GetObject();
            _lessonManagement = lessonManagementProxy.GetObject();
            _resource = resourceProxy.GetObject();
        }

        /// <summary>
        /// 下载本地资料
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        public MaterialDownloadResult DownLoadLocalMaterial(String resourceId)
        {
            var result = new MaterialDownloadResult();

            var resourceInfo = _resourceInfo.GetSimpleInfo(resourceId);
            var resourceUsageInfo = _resourceUsage.Get(new ResourceUsageGetParam()
            {
                ResourceIds = new List<string>() { resourceId },
                Usages = new List<string>() { Const.ResourceUsages.Download }
            })[0];

            var handle = resourceUsageInfo.Usages[0].Data;
            result.MaterialStream = _iOProvider.Open(handle, FileMode.Open, FileAccess.Read, FileShare.Read);
            result.MaterialName = resourceInfo.UserData.ResourceName;

            return result;
        }

        /// <summary>
        /// 学生获取课程内容(手机)
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        public LessonContent GetLessonContent(String lessonId, String token)
        {
            var result = new LessonContent();
            var teachingSpaceLessonInfo = this.GetLessonInfo(lessonId);
            result.CourseName = teachingSpaceLessonInfo.AdditionalName;
            result.LessonName = teachingSpaceLessonInfo.DisplayName;

            var lessonContents = _lessonContent.GetLessonContent(lessonId);
            var resourceIds = lessonContents.Select(l => l.RelativeId).ToList();
            if (resourceIds.Any())
            {
                var lessonResourceIds = lessonContents.Where(l => l.ContentType == Generic.TeachingSpace.Const.LessonContentType.Material).Select(l => l.RelativeId).ToList();
                var previewUrl = String.Empty;
                if (lessonResourceIds.Any())
                {
                    var interactionSection = _configSource.GetSection<InteractionConfigSection>(InteractionConfigSection.DefaultSectionName);
                    var frontSection = _configSource.GetSection<FrontRouteSection>("FrontRoute");
                    var urlMap = frontSection.RouteTables[0].EntranceTable.ToDictionary(e => e.Key, e => EnvironmentVariableConverter.Translate(e.Value));
                    previewUrl = urlMap[interactionSection.MaterialPreviewEntranceName];
                }
                // 获取一组资源信息
                var resourceInfos = _resourceInfo.GetFullInfo(new Generic.Bank.Resource.Param.ResourceInfoGetParam()
                {
                    IsIncludeAttributes = true,
                    ResourceIds = resourceIds
                });
                result.Materials = resourceInfos.Select(r => new MaterialInfo()
                {
                    Id = r.ResourceId,
                    Name = r.UserData.ResourceName,
                    Type = r.Attributes[Const.ResourceAttributes.ContentType],
                    PreviewUrl = lessonResourceIds.Contains(r.ResourceId) ? this.GetPreviewUrl(previewUrl, r.ResourceId, token) : String.Empty,
                    CanPreview = lessonResourceIds.Contains(r.ResourceId),
                    CanDownload = !lessonResourceIds.Contains(r.ResourceId)
                }).ToList();
            }

            return result;
        }

        /// <summary>
        /// 获取课程资料
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        public LessonMaterial GetLessonMaterial(String lessonId)
        {
            var result = new LessonMaterial();
            var teachingSpaceLessonInfo = this.GetLessonInfo(lessonId);
            var catalogItemId = teachingSpaceLessonInfo.RelativeCatalogId;
            result.LessonName = teachingSpaceLessonInfo.DisplayName;
            // 复合查询资源，得到资源标识列表
            var queryResult = _resourceQuery.ComplexQuery(new List<Query.Base.Param.QueryParameter>()
            {
                new Query.Base.Param.QueryParameter()
                {
                    Conditions = new AndOperator()
                    {
                        OperatorExpression = new List<IConditionParameter>()
                        {
                            new EqualOperator(){ Name = QueryName.CatalogItemId, Value = catalogItemId }
                        }
                    }
                }
            });
            if (queryResult.Count > 0 && queryResult[0].TotalCount > 0)
            {
                var lessonResourceIds = queryResult[0].Results;
                // 获取课时内容，提取下发的资料标识列表
                var lessonContents = _lessonContent.GetLessonContent(lessonId);
                var openedLessonResourceIds = lessonContents.Where(l => l.ContentType == Generic.TeachingSpace.Const.LessonContentType.Material)
                    .Select(l => l.RelativeId).ToList();
                // 获取一组资源信息
                var resourceInfos = _resourceInfo.GetFullInfo(new Generic.Bank.Resource.Param.ResourceInfoGetParam()
                {
                    IsIncludeAttributes = true,
                    ResourceIds = lessonResourceIds
                });
                result.Materials = resourceInfos.Select(r => new CourseMaterialInfo() 
                {
                    CreateTime = r.BasicData.CreateTime,
                    ModifyTime = r.BasicData.LastModifyTime,
                    Id = r.ResourceId,
                    Name = r.UserData.ResourceName,
                    Type = r.Attributes.ContainsKey(Const.ResourceAttributes.ContentType) ? r.Attributes[Const.ResourceAttributes.ContentType] : String.Empty,
                    IsOpen = openedLessonResourceIds.Contains(r.ResourceId)
                }).ToList();
            }

            return result;
        }

        /// <summary>
        /// 获取课程资料状态
        /// </summary>
        /// <param name="classroomId"></param>
        /// <returns></returns>
        public LessonMaterialStatus GetLessonMaterialStatus(String classroomId)
        {
            var result = new LessonMaterialStatus();
            var classroomWorkInfo = _classroom.GetClassroomWorkInfo(new Generic.ClassroomTeaching.Lesson.Param.ClassroomWorkInfoGetParam()
            {
                TeachingSpaceId = classroomId
            });
            if (classroomWorkInfo.IsOnClass)
            {
                result.IsInClass = classroomWorkInfo.IsOnClass;
                result.LessonId = classroomWorkInfo.ActiveLesson.LessonId;
                if (classroomWorkInfo.ActiveLesson.IsRelateCloud)
                {
                    result.HasMaterials = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 下发课程资料
        /// </summary>
        /// <param name="lessonId"></param>
        /// <param name="resourceId"></param>

        public void OpenLessonMaterial(String lessonId, String resourceId)
        {
            // 获取资源信息得到资源名
            var resourceSimpleInfo = _resourceInfo.GetSimpleInfo(resourceId);
            var resourceName = resourceSimpleInfo.UserData.ResourceName;
            var behaviorCreateResult = _batchBehavior.Create(new List<BehaviorCreateParam>()
            {
                new BehaviorCreateParam()
                {
                    Action = String.Join("_", BehaviorActionNames.OpenLessonMaterial, resourceId),
                    Principal = new BehaviorPrincipal()
                    {
                        Type = Generic.Behavior.Single.Const.PrincipalType.MySelf
                    },
                    Reception = new BehaviorReception()
                    {
                        Data = lessonId,
                        Type = ReceptionType.Identify
                    },
                    Exclusive = new BehaviorExclusive()
                    {
                        ActionList = new List<String>(){ String.Join("_", BehaviorActionNames.OpenLessonMaterial, resourceId) },
                        ActionRange = ActionExclusiveRange.ActionList,
                        PrincipalRange = PrincipalExclusiveRange.Global
                    }
                }
            })[0];

            if (behaviorCreateResult.Success)
            {
                _lessonContent.AddLessonContent(lessonId, new List<Generic.TeachingSpace.Struct.LessonContentInfo>()
                {
                    new Generic.TeachingSpace.Struct.LessonContentInfo()
                    {
                        ContentId = behaviorCreateResult.BehaviorId,
                        ContentType = Generic.TeachingSpace.Const.LessonContentType.Material,
                        RelativeId = resourceId,
                        DisplayName = resourceName
                    }
                });
            }
            else
            {
                throw new Exception("Behavior Create Error");
            }

        }

        /// <summary>
        /// 获取下发的本地资料
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        public List<LocalMaterial> GetOpenedLocalMaterials(String lessonId)
        {
            var result = new List<LocalMaterial>();
            var lessonContents =  _lessonContent.GetLessonContent(lessonId);
            var localResourceIds = lessonContents.Where(l => l.ContentType == Generic.TeachingSpace.Const.LessonContentType.Other)
                .Select(l => l.RelativeId).ToList();

            if (localResourceIds.Any())
            {
                var resourceInfos = _resourceInfo.GetFullInfo(new Generic.Bank.Resource.Param.ResourceInfoGetParam()
                {
                    IsIncludeAttributes = true,
                    ResourceIds = localResourceIds
                });
                result = resourceInfos.Select(r => new LocalMaterial()
                {
                    LastModifyTime = DateTime.ParseExact(r.Attributes[Const.ResourceAttributes.LastModifyTime], "O", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal),
                    Path = r.Attributes[Const.ResourceAttributes.Path],
                    Type = r.Attributes[Const.ResourceAttributes.ContentType]
                }).ToList();
            }
            return result;
        }

        /// <summary>
        /// 预览课程资料
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        public MaterialPreviewResult PreviewLessonMaterial(String resourceId)
        {
            var result = new MaterialPreviewResult();
            var now = _timeProvider.GetNow();
            var behaviorCreateResults = _batchBehavior.Create(new List<BehaviorCreateParam>(){
                new BehaviorCreateParam()
                {
                    Action = BehaviorActions.ViewLessonMaterial,
                    Exclusive = new BehaviorExclusive()
                    {
                        ActionList = new List<String>() { Const.BehaviorActionNames.PreviewLessonMaterial },
                        ActionRange = ActionExclusiveRange.SingleAction,
                        PrincipalRange = PrincipalExclusiveRange.Global
                    },
                    Principal = new BehaviorPrincipal()
                    {
                        Type = Generic.Behavior.Single.Const.PrincipalType.MySelf
                    },
                    Reception = new BehaviorReception()
                    {
                        Data = $"res://{resourceId}/CONTENTRELEVANCE",
                        Type = ReceptionType.UserDocument
                    },
                    Limit = new BehaviorLimit()
                    {
                        ExpireTime = now.AddDays(1)
                    }
                } 
            });
            if (behaviorCreateResults.Any()){
                if (behaviorCreateResults[0].Success)
                {
                    result.BehaviorId = behaviorCreateResults[0].BehaviorId;
                }
                else if (!String.IsNullOrWhiteSpace(behaviorCreateResults[0].OccupiedBehaviorId))
                {
                    result.BehaviorId = behaviorCreateResults[0].OccupiedBehaviorId;
                }
                else
                {
                    throw new Exception("Behavior Create Error");
                }
            }

            return result;
        }

        /// <summary>
        /// 下发本地资料
        /// </summary>
        /// <param name="lessonId"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public void OpenLocalMaterial(String lessonId, LocalMaterialOpenParam parameter)
        {
            var resourceIds = _resource.Create(new List<ResourceCreateParam>() {
                new ResourceCreateParam()
                {
                    Attributes = new Dictionary<string, string>()
                    {
                        { Const.ResourceAttributes.ContentType, this.CovertLocalMaterialType(parameter.FullPath) },
                        { Const.ResourceAttributes.LastModifyTime, parameter.ModifyTime.ToString("O") },
                        { Const.ResourceAttributes.Path, parameter.FullPath }
                    },
                    InitialUserData = new ResourceUserData()
                    {
                        ResourceName = parameter.Name,
                    }
                } }).ToList();
            // 根据资源用途的数据设备为文件句柄
            var resourceUsageUpdateParams = new List<ResourceUsageUpdateParam>();
            if(resourceIds.Any())
            {
                var resourceId = resourceIds[0];
                var handle = parameter.Handle;
                resourceUsageUpdateParams.Add(new ResourceUsageUpdateParam()
                {
                    ResourceIds = new List<string>() { resourceId },
                    UpdateParams = new List<ResourceUsage>()
                    {
                        new ResourceUsage()
                        {
                            Data = handle,
                            Usage = Const.ResourceUsages.Download,
                            UsageType = UsageType.UserDefined
                        }
                    }
                });
                _resourceUsage.Update(resourceUsageUpdateParams);

                var behaviorCreateResult = _batchBehavior.Create(new List<BehaviorCreateParam>()
                {
                    new BehaviorCreateParam()
                    {
                        Action = String.Join("_", BehaviorActionNames.OpenLocalMaterial, resourceId),
                        Principal = new BehaviorPrincipal()
                        {
                            Type = Generic.Behavior.Single.Const.PrincipalType.MySelf
                        },
                        Reception = new BehaviorReception()
                        {
                            Data = lessonId,
                            Type = ReceptionType.Identify
                        }
                    }
                })[0];

                if (behaviorCreateResult.Success)
                {
                    _lessonContent.AddLessonContent(lessonId, new List<Generic.TeachingSpace.Struct.LessonContentInfo>()
                    {
                        new Generic.TeachingSpace.Struct.LessonContentInfo()
                        {
                            ContentId = behaviorCreateResult.BehaviorId,
                            ContentType = Generic.TeachingSpace.Const.LessonContentType.Other,
                            RelativeId = resourceId,
                            DisplayName = parameter.Name
                        }
                    });
                }
            }
        }

        private String CovertLocalMaterialType(String fullPath)
        {
            var type = MaterialTypes.Other;
            var extension = Path.GetExtension(fullPath);
            switch (extension.ToLower())
            {
                case ".mp4":
                case ".mp3":
                    type = MaterialTypes.Media;
                    break;
                case ".txt":
                case ".pdf":
                case ".doc":
                case ".docx":
                case ".ppt":
                case ".pptx":
                case ".xls":
                case ".xlsx":
                    type = MaterialTypes.Document;
                    break;
                default:
                    break;
            }
            return type;
        }

        private Generic.TeachingSpace.Struct.LessonInfo GetLessonInfo(String lessonId)
        {
            if (String.IsNullOrWhiteSpace(lessonId))
            {
                throw new ArgumentException("课时标识不能为空");
            }
            var lessonSummary = _lessonManagement.GetLessonSummary(lessonId);
            if (lessonSummary.Info == null)
            {
                throw new ArgumentException("课时信息不存在");
            }
            return lessonSummary.Info;
        }

        private String GetPreviewUrl(String url, String resourceId, String token)
        {
            return String.Format(url, resourceId, token);
        }
    }
}