using Ionic.Zip;
using LINDGE.PARA.Generic.Bank.Resource.Const;
using LINDGE.PARA.Generic.Bank.Resource.Param;
using LINDGE.PARA.Generic.Bank.Resource.Struct;
using LINDGE.PARA.Generic.Bank.Resource.WebAPI.Basic;
using LINDGE.PARA.Generic.Bank.Resource.WebAPI.Query;
using LINDGE.PARA.Generic.Classroom.LibraryBuilding.Const;
using LINDGE.PARA.Generic.Classroom.LibraryBuilding.Service.Interface;
using LINDGE.PARA.Generic.Register.Census.API.Basic;
using LINDGE.PARA.Generic.Register.Census.Param;
using LINDGE.PARA.Generic.Register.Census.Struct;
using LINDGE.PARA.Generic.Sociality.User.Struct;
using LINDGE.PARA.Generic.TeachingSpace.Struct;
using LINDGE.PARA.Generic.TeachingSpace.WebAPI.Lesson;
using LINDGE.PARA.GenericUpdate.Base.Param;
using LINDGE.PARA.Query.Base.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Struct;
using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.WebAPI.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.WebAPI.Model;
using LINDGE.PARA.Translayer.Static.Picture.WebAPI;
using LINDGE.PARA.Translayer.Static.Picture.WebAPI.Param;
using LINDGE.PARA.Translayer.Static.Picture.WebAPI.Struct;
using LINDGE.Proxy;
using LINDGE.Proxy.WebAPIClient;
using LINDGE.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Callback.WebAPI.Service
{
    public class ImportLessonService : IImportLessonService
    {
        private readonly IIOProvider _iOProvider = null;
        private readonly IConfigSource _configSource = null;
        private readonly ILessonActiviry _lessonActiviry = null;
        private readonly IPictureSet _pictureSet = null;
        private readonly IResourceUsage _resourceUsage = null;
        private readonly IResource _resource = null;
        private readonly IResourceQuery _resourceQuery = null;
        private readonly IBatchLesson _batchLesson = null;
        private readonly IIndividual _individual = null;
        private readonly ILessonSync _lessonSync = null;

        public ImportLessonService(IIOProvider iOProvider,
            IConfigSource configSource,
            ILessonActiviry lessonActiviryService,
            IProxy<IPictureSet> pictureStorage,
            IProxy<IResourceUsage> resourceUsage,
            IProxy<IResource> resource,
            IProxy<IResourceQuery> resouceQuery,
            IProxy<IBatchLesson> batchLessonProxy,
            IProxy<IIndividual> individualProxy,
            IProxy<ILessonSync> lessonSyncPorxy)
        {
            _iOProvider = iOProvider;
            _configSource = configSource;
            _lessonActiviry = lessonActiviryService;
            _pictureSet = pictureStorage.GetObject();
            _resourceUsage = resourceUsage.GetObject();
            _resource = resource.GetObject();
            _resourceQuery = resouceQuery.GetObject();
            _batchLesson = batchLessonProxy.GetObject();
            _individual = individualProxy.GetObject();
            _lessonSync = lessonSyncPorxy.GetObject();
        }

        /// <summary>
        /// 同步云端课时信息
        /// </summary>
        /// <param name="parameter"></param>
        public void SyncCloudLesson(Param.CloudLessonSyncParam parameter)
        {
            var teachingClassConfig = new TeachingClassConfig();
            // 检查句柄文件是否存在
            if (!_iOProvider.Exists(parameter.Handle))
            {
                throw new Exception("fileHandle is not exist");
            }
            using (var stream = _iOProvider.Open(parameter.Handle, FileMode.Open, FileAccess.Read))
            {
                if (stream.CanSeek)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                }
                // 读取压缩包
                using (var zip = ZipFile.Read(stream, new ReadOptions() { Encoding = Encoding.UTF8 }))
                {
                    // 获取压缩包中的教学班信息配置文件
                    var entry = zip.SelectEntries("Lesson.json").FirstOrDefault();
                    if (entry == null)
                    {
                        throw new Exception("lessonFile is not exist");
                    }
                    using (MemoryStream ms = new MemoryStream())
                    {
                        entry.Extract(ms);
                        ms.Seek(0, SeekOrigin.Begin);
                        var content = new StreamReader(ms, Encoding.UTF8).ReadToEnd();
                        teachingClassConfig.FromJSONString(content);
                    }
                }
            }
            _lessonSync.Import(new LessonSyncRecordData()
            {
                TargetLessonId = parameter.LessonId,
                SourceSpaceId = teachingClassConfig.LessonSyncRecord.SourceSpaceId,
                SourceLessonId = teachingClassConfig.LessonSyncRecord.SourceLessonId,
                ExportTime = teachingClassConfig.LessonSyncRecord.ExportTime
            });
            _batchLesson.ModifyLesson(parameter.ClassroomId, new List<Generic.TeachingSpace.Param.LessonModifyParam>()
            {
                new Generic.TeachingSpace.Param.LessonModifyParam()
                {
                    ModifyFlags = Generic.TeachingSpace.Const.ModifyFlag.Cover,
                    LessonCover = teachingClassConfig.CoverId,
                    LessonId = parameter.LessonId
                }
            });
        }

        /// <summary>
        /// 添加课时资料
        /// </summary>
        /// <param name="parameter"></param>
        public void AddLessonMaterial(LessonMaterialAddParam parameter)
        {
            var destDirectory = ImportLessonConst.UploadFileUrl;
            var resourceDetailInfo = new ResourceDetailInfo();
            var resourceUsages = new List<Model.ResourceUsage>();
            // 检查句柄文件是否存在
            if (!_iOProvider.Exists(parameter.Handle))
            {
                throw new Exception("fileHandle is not exist");
            }
            using (var stream = _iOProvider.Open(parameter.Handle, FileMode.Open, FileAccess.Read))
            {
                if (stream.CanSeek)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                }
                // 读取压缩包
                using (var zip = ZipFile.Read(stream, new ReadOptions() { Encoding = Encoding.UTF8 }))
                {
                    var entries = zip.Entries.Where(e => !e.IsDirectory && e.FileName.StartsWith(parameter.FolderName));
                    var resourceInfoEntry = entries.First(e => e.FileName.EndsWith("ResourceInfo.json"));
                    using (MemoryStream ms = new MemoryStream())
                    {
                        resourceInfoEntry.Extract(ms);
                        ms.Seek(0, SeekOrigin.Begin);
                        string content = new StreamReader(ms, Encoding.UTF8).ReadToEnd();
                        resourceDetailInfo.FromJSONString(content);
                    }
                    var resourceId = GetResourceId(resourceDetailInfo.Relation.GlobalId);
                    // 资源是否存在
                    if (String.IsNullOrWhiteSpace(resourceId))
                    {
                        foreach (var entry in entries)
                        {
                            if (!entry.FileName.EndsWith("ResourceInfo.json"))
                            {
                                var fileName = Path.GetFileNameWithoutExtension(entry.FileName);
                                var fileUrl = $"{destDirectory}{resourceDetailInfo.ResourceId}_{fileName}";
                                Unzip(entry, fileUrl);
                                resourceUsages.Add(new Model.ResourceUsage()
                                {
                                    Usage = fileName,
                                    PackageUrl = fileUrl
                                });
                            }
                        }
                        resourceDetailInfo.ResourceUsages = resourceUsages;
                        resourceId = CreateResource(resourceDetailInfo);
                    }
                    _lessonActiviry.Add(parameter.LibraryId, new List<Generic.Classroom.LibraryBuilding.Param.LessonActivityAddParam>()
                    {
                        new Generic.Classroom.LibraryBuilding.Param.LessonActivityAddParam()
                        {
                            Activities = new List<Generic.Classroom.LibraryBuilding.Param.ActivityAddParam>()
                            {
                                new Generic.Classroom.LibraryBuilding.Param.ActivityAddParam()
                                {
                                    ResourceId = resourceId,
                                    AnswerMode = resourceDetailInfo.Attributes.ContainsKey(ResourceAttributeNames.ActivityAnswerMode) 
                                    ? resourceDetailInfo.Attributes[ResourceAttributeNames.ActivityAnswerMode]: String.Empty,
                                    Scene = resourceDetailInfo.Attributes.ContainsKey(ResourceAttributeNames.ActivityScene)
                                    ? resourceDetailInfo.Attributes[ResourceAttributeNames.ActivityScene]: String.Empty,
                                    Source = resourceDetailInfo.Attributes.ContainsKey(ResourceAttributeNames.ActivitySource)
                                    ? resourceDetailInfo.Attributes[ResourceAttributeNames.ActivitySource]: String.Empty,
                                    ContentType = resourceDetailInfo.Attributes.ContainsKey(ResourceAttributeNames.ActivityContentType)
                                    ? resourceDetailInfo.Attributes[ResourceAttributeNames.ActivityContentType]: String.Empty,
                                }
                            },
                            LessonId = parameter.CatalogItemId
                        }
                    });
                }
            }
        }

        private String CreateResource(ResourceDetailInfo resourceDetailInfo)
        {
            var initialUserData = new Generic.Bank.Resource.Struct.ResourceUserData()
            {
                ItemTemplate = resourceDetailInfo.UserData.ItemTemplate,
                ItemTypeName = resourceDetailInfo.UserData.ItemTypeName,
                ResourceName = resourceDetailInfo.UserData.ResourceName,
                ResourceType = resourceDetailInfo.UserData.ResourceType,
                Source = resourceDetailInfo.UserData.Source
            };

            var resourceattributes = resourceDetailInfo.Attributes;
            var resourceRelationData = new ResourceRelationData()
            {
                GlobalId = resourceDetailInfo.Relation.GlobalId,
                SourceGlobalId = resourceDetailInfo.Relation.SourceGlobalId
            };

            var resourceUsages = resourceDetailInfo.ResourceUsages.Select(resourceUsage => new Generic.Bank.Resource.Struct.ResourceUsage()
            {
                UsageType = UsageType.Document,
                Usage = resourceUsage.Usage,
                Data = GetZipPackageUrl(resourceUsage.PackageUrl)
            }).ToList();

            var resourceId = _resource.Create(new List<ResourceCreateParam>()
            {
                new ResourceCreateParam() {
                    SpecifiableResourceId = null,
                    InitialUserData =initialUserData,
                    Attributes = resourceattributes,
                    InitialRelationData = new ResourceRelationData() { GlobalId = resourceRelationData.GlobalId,SourceGlobalId=resourceRelationData.SourceGlobalId },
                }
            }).FirstOrDefault();

            foreach (var resourceUsage in resourceUsages)
            {
                _resourceUsage.Update(new List<ResourceUsageUpdateParam>()
                {
                    new ResourceUsageUpdateParam()
                    {
                        ResourceIds = new List<string>() { resourceId },
                        UpdateParams = new List<Generic.Bank.Resource.Struct.ResourceUsage>() { resourceUsage }
                    }
                });

            }
            return resourceId;
        }

        private String GetZipPackageUrl(String sourceUrl)
        {
            return sourceUrl.Replace(ImportLessonConst.FileProtocol, ImportLessonConst.ZipProtocol);
        }

        private void Unzip(ZipEntry entry, String fileUrl)
        {
            using (var fs = this._iOProvider.Open(fileUrl, FileMode.Create, FileAccess.Write))
            {
                entry.Extract(fs);
                fs.Seek(0, SeekOrigin.Begin);
                fs.Flush();
                fs.Close();
            }
        }

        private String GetResourceId(String globalId)
        {
            var resourceQueryOperator = new AndOperator();
            resourceQueryOperator.OperatorExpression = new List<IConditionParameter>();
            resourceQueryOperator.OperatorExpression.Add(new EqualOperator()
            {
                Name = Generic.Bank.Resource.Query.QueryName.GlobalId,
                Value = globalId
            });
            var queryParameter = new QueryParameter()
            {
                Conditions = resourceQueryOperator
            };

            var queryResult = _resourceQuery.ComplexQuery(new List<QueryParameter>() { queryParameter });
            return queryResult[0].TotalCount > 0 ? queryResult[0].Results.FirstOrDefault() : String.Empty;
        } 

        /// <summary>
        /// 导入学生名单
        /// </summary>
        /// <param name="parameter"></param>
        public void ImportStudent(StudentImportParam parameter)
        {
            var students = new List<StudentInfo>();
            // 检查句柄文件是否存在
            if (!_iOProvider.Exists(parameter.Handle))
            {
                throw new Exception("fileHandle is not exist");
            }
            using (var stream = _iOProvider.Open(parameter.Handle, FileMode.Open, FileAccess.Read))
            {
                if (stream.CanSeek)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                }
                // 读取压缩包
                using (var zip = ZipFile.Read(stream, new ReadOptions() { Encoding = Encoding.UTF8 }))
                {
                    // 获取压缩包中的学生名单信息
                    var entry = zip.SelectEntries("NameList.json").FirstOrDefault();
                    if (entry == null)
                    {
                        throw new Exception("fileHandle is not exist");
                    }
                    using (MemoryStream ms = new MemoryStream())
                    {
                        entry.Extract(ms);
                        ms.Seek(0, SeekOrigin.Begin);
                        var content = new StreamReader(ms, Encoding.UTF8).ReadToEnd();
                        students.FromJSONString(content);
                    }
                    students = students.OrderBy(s => s.Number).Skip(parameter.Index).Take(parameter.StudentCount).ToList();
                }
            }
            for (var index = 0; index < students.Count; index++)
            {
                students[index].PortraitId = String.IsNullOrWhiteSpace(students[index].PortraitId)
                    ? String.Empty : students[index].PortraitId;
            }
            var memberInfos = students.Select(s => new StudentFullInfo()
            {
                Gender = ConvertEnumFromString(s.Gender),
                Name = s.Name,
                Number = s.Number,
                Portrait = s.PortraitId,
                CorrelativeCode = s.CorrelativeCode,
                MemberId = s.MemberId,
                IsLogon = "false",
                IsSigned = "false",
                LatestActiveTime = String.Empty
            }).ToList();
            this.UpdateCensus(parameter.CensusId, memberInfos);
        }

        /// <summary>
        /// 导入图片
        /// </summary>
        /// <param name="parameter"></param>
        public void ImportPicture(PictureImportParam parameter)
        {
            var pictureSetParams = new PictureSetImportParam()
            {
                PictureSets = new List<FileData>()
            };
            // 检查句柄文件是否存在
            if (!_iOProvider.Exists(parameter.Handle))
            {
                throw new Exception("fileHandle is not exist");
            }
            using (var stream = _iOProvider.Open(parameter.Handle, FileMode.Open, FileAccess.Read))
            {
                if (stream.CanSeek)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                }
                // 读取压缩包
                using (var zip = ZipFile.Read(stream, new ReadOptions() { Encoding = Encoding.UTF8 }))
                {
                    // 获取学生头像文件名列表
                    foreach (var pictureEntryName in parameter.PictureEntryNames)
                    {
                        var portraitEntry = zip.Entries.First(e => !e.IsDirectory && e.FileName == pictureEntryName);
                        pictureSetParams.PictureSets.Add(new FileData()
                        {
                            Data = Convert.ToBase64String(ConvertByte(portraitEntry))
                        });
                    }
                }
            }
            if (pictureSetParams.PictureSets.Any())
            {
                _pictureSet.Import(pictureSetParams);
            }
        }

        private byte[] ConvertByte(ZipEntry entry)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                entry.Extract(ms);
                ms.Seek(0, SeekOrigin.Begin);
                byte[] bytes = new byte[ms.Length];
                ms.Read(bytes, 0, bytes.Length);
                ms.Seek(0, SeekOrigin.Begin);
                return bytes;
            }
        }
        /// <summary>
        /// 性别字符串转枚举
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private Struct.Gender ConvertEnumFromString(String value)
        {
            var gender = Struct.Gender.Unknown;
            switch (value)
            {
                case GenderConstants.UnknownCode: gender = Struct.Gender.Unknown; break;
                case GenderConstants.MaleCode: gender = Struct.Gender.Male; break;
                case GenderConstants.FemaleCode: gender = Struct.Gender.Female; break;
            }
            return gender;
        }

        private void UpdateCensus(String censusId, List<Struct.StudentFullInfo> memberSummaryInfos)
        {
            // 更新名单信息   该接口会将已存在的信息更新，不存在的追加
            var individualUpdateItems = new List<IndividualUpdateItem>();
            foreach (var memberSummaryInfo in memberSummaryInfos)
            {
                var individualDataUpdateItems = new List<IndividualDataUpdateItem>()
                {
                    new IndividualDataUpdateItem(){ FieldName = StudentFieldNames.CorrelativeCode, FieldValue = memberSummaryInfo.CorrelativeCode },
                    new IndividualDataUpdateItem(){ FieldName = StudentFieldNames.Gender, FieldValue = memberSummaryInfo.Gender.ToString() },
                    new IndividualDataUpdateItem(){ FieldName = StudentFieldNames.IP, FieldValue = HttpRequestExtension.GetRemoteAddress() }
                };

                if (memberSummaryInfo.MemberId != null)
                    individualDataUpdateItems.Add(new IndividualDataUpdateItem() { FieldName = StudentFieldNames.LocalMemberId, FieldValue = memberSummaryInfo.MemberId });
                if (memberSummaryInfo.Name != null)
                    individualDataUpdateItems.Add(new IndividualDataUpdateItem() { FieldName = StudentFieldNames.Name, FieldValue = memberSummaryInfo.Name });
                if (memberSummaryInfo.Number != null)
                    individualDataUpdateItems.Add(new IndividualDataUpdateItem() { FieldName = StudentFieldNames.Number, FieldValue = memberSummaryInfo.Number });
                if (memberSummaryInfo.Portrait != null)
                    individualDataUpdateItems.Add(new IndividualDataUpdateItem() { FieldName = StudentFieldNames.Portrait, FieldValue = memberSummaryInfo.Portrait });
                if (memberSummaryInfo.LatestActiveTime != null)
                    individualDataUpdateItems.Add(new IndividualDataUpdateItem() { FieldName = StudentFieldNames.LatestActiveTime, FieldValue = memberSummaryInfo.LatestActiveTime });
                if (memberSummaryInfo.IsLogon != null)
                    individualDataUpdateItems.Add(new IndividualDataUpdateItem() { FieldName = StudentFieldNames.IsLogon, FieldValue = memberSummaryInfo.IsLogon });
                if (memberSummaryInfo.IsSigned != null)
                    individualDataUpdateItems.Add(new IndividualDataUpdateItem() { FieldName = StudentFieldNames.IsSigned, FieldValue = memberSummaryInfo.IsSigned });

                individualUpdateItems.Add(new IndividualUpdateItem()
                {
                    DataUpdateParam = new GenericListUpdateParam<IndividualDataUpdateItem>()
                    {
                        UpdateParams = individualDataUpdateItems
                    },
                    UniqueCode = memberSummaryInfo.CorrelativeCode
                });
            }
            _individual.Update(new List<IndividualUpdateParam>()
            {
                new IndividualUpdateParam()
                {
                    CensusIds = new List<string>(){ censusId },
                    UpdateParams = individualUpdateItems,
                }
            });
        }
    }
}