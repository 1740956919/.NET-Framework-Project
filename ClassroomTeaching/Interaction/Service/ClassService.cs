using Ionic.Zip;
using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Fundamental.Message.Struct;
using LINDGE.PARA.Fundamental.Message.WebAPI.Basic;
using LINDGE.PARA.Fundamental.Runtime.CodeGenerate;
using LINDGE.PARA.Generic.Behavior.Collaboration.API.Basic;
using LINDGE.PARA.Generic.Behavior.Collaboration.Param;
using LINDGE.PARA.Generic.Behavior.Collaboration.Struct;
using LINDGE.PARA.Generic.Behavior.Single.API.Basic;
using LINDGE.PARA.Generic.Behavior.Single.API.Query;
using LINDGE.PARA.Generic.Behavior.Single.Const;
using LINDGE.PARA.Generic.Behavior.Single.Param;
using LINDGE.PARA.Generic.Behavior.Single.Struct;
using LINDGE.PARA.Generic.Classroom.LibraryBuilding.Service.Interface;
using LINDGE.PARA.Generic.ClassroomTeaching.Lesson.Const;
using LINDGE.PARA.Generic.ClassroomTeaching.Lesson.Service.Interface;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Struct;
using LINDGE.PARA.Generic.Register.Census.API.Basic;
using LINDGE.PARA.Generic.Register.Census.API.Query;
using LINDGE.PARA.Generic.Register.Census.Const;
using LINDGE.PARA.Generic.Register.Census.Param;
using LINDGE.PARA.Generic.Register.Census.Struct;
using LINDGE.PARA.Generic.Sociality.Group.API.Get.RoleSet;
using LINDGE.PARA.Generic.Sociality.Group.API.Query.MemberRole;
using LINDGE.PARA.Generic.Sociality.Group.Param;
using LINDGE.PARA.Generic.Sociality.User.API.Logon;
using LINDGE.PARA.Generic.Sociality.User.API.Profile;
using LINDGE.PARA.Generic.Sociality.User.Struct;
using LINDGE.PARA.Generic.TeachingSpace.Const;
using LINDGE.PARA.Generic.TeachingSpace.Struct;
using LINDGE.PARA.Generic.TeachingSpace.WebAPI.Lesson;
using LINDGE.PARA.Generic.TeachingSpace.WebAPI.TeachingSpace;
using LINDGE.PARA.GenericUpdate.Base.Param;
using LINDGE.PARA.Query.Base.Param;
using LINDGE.PARA.Runtime;
using LINDGE.PARA.Runtime.BehaviorJob;
using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Struct;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interation.WebAPI.Result;
using LINDGE.PARA.Translayer.Static.Picture.WebAPI;
using LINDGE.PARA.Translayer.Static.Picture.WebAPI.Param;
using LINDGE.Proxy;
using LINDGE.Proxy.WebAPIClient;
using LINDGE.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Service
{
    /// <summary>
    /// 班级管理服务
    /// </summary>
    public class ClassService : IClassService
    {
        private readonly IConfigSource _configSource = null;
        private readonly IIOProvider _iOProvider = null;
        private readonly ILesson _lesson = null;
        private readonly IClassroom _classroom = null;
        private readonly ILessonMember _lessonMember = null;
        private readonly ITimeProvider _timeProvider = null;
        private readonly IBehaviorQuery _behaviorQuery = null;
        private readonly IBehaviorCompletion _behaviorCompletion = null;
        private readonly IBatchBehavior _batchBehavior = null;
        private readonly IBehaviorExecution _behaviorExecution = null;
        private readonly IQueryProfileById _queryProfileById = null;
        private readonly IBehaviorInfo _behaviorInfo = null;
        private readonly ICensus _census = null;
        private readonly ILessonActivity _lessonActivity = null;
        private readonly IDeviceLogon _deviceLogon = null;
        private readonly IMessage _message = null;
        private readonly ITeachingSpace _teachingSpace = null;
        private readonly ILessonManagement _lessonManagement = null;
        private readonly IQueryUserMemberRole _queryUserMemberRole = null;
        private readonly ICensusQuery _censusQuery = null;
        private readonly IUniqueCode _uniqueCode = null;
        private readonly IProfile _profile = null;
        private readonly IPasswordLogon _passwordLogon = null;
        private readonly IBatchUserDevice _batchUserDevice = null;
        private readonly IIndividual _individual = null;
        private readonly IEnrolledIndividual _enrolledIndividual = null;
        private readonly IEnrollment _enrollment = null;
        private readonly ILessonAttendance _lessonAttendance = null;
        private readonly IPictureSet _pictureSet = null;
        private readonly ILimitQueryProfileByProperties _limitQueryProfileByProperties = null;
        private readonly IIndividualQuery _individualQuery = null;
        private readonly ILessonSync _lessonSync = null;
        private readonly ILessonInfo _lessonInfo = null;
        private readonly IGetRoleSetPath _getRoleSetPath = null;
        private readonly IBatchCollaboration _batchCollaboration = null;
        private readonly ICollaborationCompletion _collaborationCompletion = null;

        public ClassService(
            IConfigSource configSource,
            IIOProvider iOProvider,
            ILesson lesson,
            IClassroom classroomService,
            ILessonMember lessonMemberService,
            ITimeProvider timeProvider,
            IProxy<IBehaviorQuery> behaviorQuery,
            IProxy<IBehaviorCompletion> behaviorCompletion,
            IProxy<IBatchBehavior> batchBehavior,
            IProxy<IBehaviorExecution> behaviorExecution,
            IProxy<IQueryProfileById> queryProfileByIdProxy,
            IProxy<IBehaviorInfo> behaviorInfoProxy,
            IProxy<ICensus> censusProxy,
            IProxy<ILessonActivity> lessonActivityProxy,
            IProxy<IDeviceLogon> deviceLogonProxy,
            IProxy<IMessage> messageProxy,
            IProxy<ITeachingSpace> teachingSpaceProxy,
            IProxy<ILessonManagement> lessonManagementProxy,
            IProxy<IQueryUserMemberRole> queryUserMemberRoleProxy,
            IProxy<ICensusQuery> censusQueryProxy,
            IProxy<IUniqueCode> uniqueCodeProxy,
            IProxy<IProfile> profileProxy,
            IProxy<IPasswordLogon> passwordLogonProxy,
            IProxy<IBatchUserDevice> batchUserDeviceProxy,
            IProxy<IIndividual> individualProxy,
            IProxy<IEnrolledIndividual> enrolledIndividualProxy,
            IProxy<IEnrollment> enrollmentProxy,
            IProxy<ILessonAttendance> lessonAttendance,
            IProxy<IPictureSet> pictureSetProxy,
            IProxy<ILimitQueryProfileByProperties> limitQueryProfileByPropertiesProxy,
            IProxy<IIndividualQuery> individualQueryProxy,
            IProxy<ILessonSync> lessonSyncProxy,
            IProxy<ILessonInfo> lessonInfoProxy,
            IProxy<IGetRoleSetPath> getRoleSetPathProxy,
            IProxy<IBatchCollaboration> batchCollaborationProxy,
            IProxy<ICollaborationCompletion> collaborationCompletionProxy)
        {
            _configSource = configSource;
            _iOProvider = iOProvider;
            _lesson = lesson;
            _classroom = classroomService;
            _lessonMember = lessonMemberService;
            _timeProvider = timeProvider;
            _behaviorQuery = behaviorQuery.GetObject();
            _behaviorCompletion = behaviorCompletion.GetObject();
            _batchBehavior = batchBehavior.GetObject();
            _behaviorExecution = behaviorExecution.GetObject();
            _queryProfileById = queryProfileByIdProxy.GetObject();
            _behaviorInfo = behaviorInfoProxy.GetObject();
            _census = censusProxy.GetObject();
            _lessonActivity = lessonActivityProxy.GetObject();
            _deviceLogon = deviceLogonProxy.GetObject();
            _message = messageProxy.GetObject();
            _teachingSpace = teachingSpaceProxy.GetObject();
            _lessonManagement = lessonManagementProxy.GetObject();
            _queryUserMemberRole = queryUserMemberRoleProxy.GetObject();
            _censusQuery = censusQueryProxy.GetObject();
            _uniqueCode = uniqueCodeProxy.GetObject();
            _profile = profileProxy.GetObject();
            _passwordLogon = passwordLogonProxy.GetObject();
            _batchUserDevice = batchUserDeviceProxy.GetObject();
            _individual = individualProxy.GetObject();
            _enrolledIndividual = enrolledIndividualProxy.GetObject();
            _enrollment = enrollmentProxy.GetObject();
            _lessonAttendance = lessonAttendance.GetObject();
            _pictureSet = pictureSetProxy.GetObject();
            _limitQueryProfileByProperties = limitQueryProfileByPropertiesProxy.GetObject();
            _individualQuery = individualQueryProxy.GetObject();
            _lessonSync = lessonSyncProxy.GetObject();
            _lessonInfo = lessonInfoProxy.GetObject();
            _getRoleSetPath = getRoleSetPathProxy.GetObject();
            _batchCollaboration = batchCollaborationProxy.GetObject();
            _collaborationCompletion = collaborationCompletionProxy.GetObject();
        }

        /// <summary>
        /// 创建导入课程job
        /// </summary>
        /// <param name="lessonId"></param>
        /// <param name="handle"></param>
        /// <param name="behaviorId"></param>
        private void CreateImportJob(String lessonId, String handle, String behaviorId, String classroomId)
        {
            var students = new List<LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Struct.StudentInfo>();
            var folderNames = new List<String>();
            var pictureEntryNames = new List<String>();
            var teachingClassConfig = new TeachingClassConfig();
            var pictureEntryNamesParam = new List<String>();
            // 获取库标识
            var teachingSpaceFullInfo = _teachingSpace.GetTeachingSpace(classroomId);
            var libraryId = teachingSpaceFullInfo.Accessories.First(a => a.AccessoryType == TeachingSpaceAccessoryType.Library).AccessoryId;
            var lessonInfo = this.GetLessonInfo(lessonId);
            var catalogItemId = lessonInfo.RelativeCatalogId;
            var censusId = this.GetCensusByLessonId(lessonId);

            using (var stream = _iOProvider.Open(handle, FileMode.Open, FileAccess.Read))
            {
                if (stream.CanSeek)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                }
                // 读取压缩包
                using (var zip = ZipFile.Read(stream, new ReadOptions() { Encoding = Encoding.UTF8 }))
                {
                    // 获取资料文件夹名列表
                    var materialPatten = @"Materials(/|\\)[\w]";
                    foreach (var fileName in zip.Entries.Where(e => e.IsDirectory).Select(e => e.FileName))
                    {
                        if (Regex.IsMatch(fileName, materialPatten))
                        {
                            folderNames.Add(fileName);
                        }
                    }
                    // 获取压缩包中的教学班信息配置文件
                    var configEntry = zip.SelectEntries("Lesson.json").FirstOrDefault();
                    if (configEntry == null)
                    {
                        throw new Exception("configFile is not exist");
                    }
                    using (MemoryStream ms = new MemoryStream())
                    {
                        configEntry.Extract(ms);
                        ms.Seek(0, SeekOrigin.Begin);
                        var content = new StreamReader(ms, Encoding.UTF8).ReadToEnd();
                        teachingClassConfig.FromJSONString(content);
                    }
                    if (String.IsNullOrWhiteSpace(teachingClassConfig.LessonName))
                    {
                        throw new Exception("lessonName is not exist");
                    }
                    // 获取压缩包中的学生名单信息
                    var entry = zip.SelectEntries("NameList.json").FirstOrDefault();
                    if (entry == null)
                    {
                        throw new HttpResponseException(new HttpResponseMessage()
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Content = new StringContent((new { Code = Const.ErrorCodes.LoseFile }).ToJSONString())
                        });
                    }
                    using (MemoryStream ms = new MemoryStream())
                    {
                        entry.Extract(ms);
                        ms.Seek(0, SeekOrigin.Begin);
                        var content = new StreamReader(ms, Encoding.UTF8).ReadToEnd();
                        students.FromJSONString(content);
                    }
                    // 提取压缩包内所有的图片entry名并去重
                    // 封面存在，则要导入封面
                    var patten = @"(Cover(/|\\)[\w]|NameList(/|\\)Portraits(/|\\)[\w])";
                    foreach (var fileName in zip.Entries.Where(e => !e.IsDirectory).Select(e => e.FileName))
                    {
                        if (Regex.IsMatch(fileName, patten))
                        {
                            pictureEntryNames.Add(fileName);
                        }
                    }
                }
            }
            // 获取课时的同步记录
            var isSyncCloudLesson = false;
            var syncRecord = _lessonSync.GetSyncRecord(lessonId);
            // 无同步记录 同步一遍
            if (syncRecord == null)
            {
                isSyncCloudLesson = true;
            }
            else
            {
                // 同一个课时 同步一遍
                if(syncRecord.SourceLessonId == teachingClassConfig.LessonSyncRecord.SourceLessonId)
                {
                    isSyncCloudLesson = true;
                }
            }
            var jobBuilder = new JobBuilder(_configSource);
            jobBuilder.SetDescription("正在导入课程");
            jobBuilder.SetWorkLane(String.Empty);
            if (isSyncCloudLesson)
            {
                // 导入图片
                for (var index = 0; index < pictureEntryNames.Count; index += 5)
                {
                    pictureEntryNamesParam = pictureEntryNames.Skip(index).Take(5).ToList();
                    jobBuilder.AddTryWebAPIStep<IImportPicture>($"正在导入图片", 0, "|Translayer.ClassroomTeaching.Callback|", "ImportPicture", new PictureImportParam()
                    {
                        Handle = handle,
                        PictureEntryNames = pictureEntryNamesParam,
                    });
                }
                // 同步课时信息
                jobBuilder.AddTryWebAPIStep<ISyncCloudLesson>($"正在同步课时信息", 0, "|Translayer.ClassroomTeaching.Callback|", "SyncCloudLesson", new CloudLessonSyncParam()
                {
                    LessonId = lessonId,
                    Handle = handle,
                    ClassroomId = classroomId
                });
                // 导入学生名单
                for (var index = 0; index < students.Count; index += 50)
                {
                    jobBuilder.AddTryWebAPIStep<IImportStudent>($"正在导入学生名单", 0, "|Translayer.ClassroomTeaching.Callback|", "ImportStudent", new StudentImportParam()
                    {
                        Handle = handle,
                        StudentCount = 50,
                        Index = index,
                        CensusId = censusId
                    });
                }
            }
            // 添加课时资料
            foreach (var folderName in folderNames)
            {
                jobBuilder.AddTryWebAPIStep<IAddLessonMaterial>($"正在添加课时资料", 0, "|Translayer.ClassroomTeaching.Callback|", "AddLessonMaterial", new LessonMaterialAddParam()
                {
                    Handle = handle,
                    FolderName = folderName.TrimEnd(new char[] { '\\', '/' }),
                    LessonId = lessonId,
                    LibraryId = libraryId,
                    CatalogItemId = catalogItemId
                });
            }
            // 结束行为
            jobBuilder.AddFinallyWebAPIStep<IBehaviorCompletion>($"结束行为", 0, "|Generic.Behavior.Single|", "Complete", new List<BehaviorCompleteParam>()
            {
                new BehaviorCompleteParam()
                {
                    BehaviorIds = new List<String>() { behaviorId },
                    ResultCode = "Complete",
                    ResultType = ResultType.UserDefine
                }
            });
            // 执行行为
            _behaviorExecution.Execute(new List<BehaviorExecuteParam>()
            {
                new BehaviorExecuteParam()
                {
                    Action = BehaviorActionNames.ImportLesson,
                    BehaviorIds = new List<String>(){ behaviorId },
                    JobParameters = new List<Engine.Job.JobParameter>()
                    {
                        jobBuilder.GetJobParameter()
                    }
                }
            });
        }

        /// <summary>
        /// 获取教室状态
        /// </summary>
        /// <param name="classroomId"></param>
        /// <returns></returns>
        public ClassroomStateInfo GetClassroomState(String classroomId)
        {
            var result = new ClassroomStateInfo();
            var classroomWorkInfo = _classroom.GetClassroomWorkInfo(new Generic.ClassroomTeaching.Lesson.Param.ClassroomWorkInfoGetParam()
            {
                TeachingSpaceId = classroomId,
                IncludeTeacher = true
            });
            if (classroomWorkInfo.IsOnClass)
            {
                result.LessonInfo.LessonId = classroomWorkInfo.ActiveLesson.LessonId;
                if (classroomWorkInfo.ActiveLesson.IsRelateCloud)
                {
                    result.HasRelatedLesson = true;
                    result.LessonInfo.Cover = classroomWorkInfo.ActiveLesson.Cover;
                    result.LessonInfo.TeachingClassName = classroomWorkInfo.ActiveLesson.TeachingClassName;
                    result.LessonInfo.LessonName = classroomWorkInfo.ActiveLesson.Name;
                    // 获取教学空间资产信息得到库标识
                    var teachingSpaceFullInfo = _teachingSpace.GetTeachingSpace(classroomId);
                    var libraryId = teachingSpaceFullInfo.Accessories.First(a => a.AccessoryType == TeachingSpaceAccessoryType.Library).AccessoryId;
                    if (!String.IsNullOrWhiteSpace(libraryId))
                    {
                        var lessonInfo = _lesson.GetLessonInfo(new List<String>() { classroomWorkInfo.ActiveLesson.LibraryCatalogItemId });
                        result.LessonInfo.CourseName = lessonInfo.FirstOrDefault(l => l.LessonId == classroomWorkInfo.ActiveLesson.LibraryCatalogItemId)?.Name;
                    }
                }
                if (!String.IsNullOrWhiteSpace(classroomWorkInfo.TeacherId))
                {
                    var profileInfoWithoutIdMap = _queryProfileById.Query(new String[] { classroomWorkInfo.TeacherId });
                    if (profileInfoWithoutIdMap.Any())
                    {
                        result.HasRelatedTeacher = true;
                        result.TeacherInfo.TeacherName = profileInfoWithoutIdMap.First().Value.DisplayName;
                        result.TeacherInfo.TeacherPortrait = profileInfoWithoutIdMap.First().Value.Portrait;
                    }
                }

                // 查询导入课程或教师扫码的行为
                var behaviorQueryResults = _behaviorQuery.ComplexQuery(new List<QueryParameter>()
                {
                    new QueryParameter()
                    {
                        Conditions = new AndOperator()
                        {
                            OperatorExpression = new List<IConditionParameter>()
                            {
                                new OrOperator
                                {
                                    OperatorExpression = new List<IConditionParameter>()
                                    {
                                        new  EqualOperator{ Name = Generic.Behavior.Single.Query.QueryName.Action, Value = BehaviorActionNames.ImportLesson },
                                        new  EqualOperator{ Name = Generic.Behavior.Single.Query.QueryName.Action, Value = BehaviorActionNames.TeacherScan }
                                    }
                                },
                                new EqualOperator { Name = Generic.Behavior.Single.Query.QueryName.ReceptionData, Value = classroomWorkInfo.ActiveLesson.LessonId },
                                new EqualOperator { Name = Generic.Behavior.Single.Query.QueryName.IsComplete, Value = "FALSE" }
                            }
                        }
                    }
                });
                if (behaviorQueryResults.Any() && behaviorQueryResults[0].TotalCount > 0)
                {
                    var behaviorId = behaviorQueryResults[0].Results[0];
                    var behaviorInfo = _behaviorInfo.GetBehaviorInfo(new List<BehaviorInfoGetParam>()
                    {
                        new BehaviorInfoGetParam()
                        {
                            IsIncludeAction = true,
                            IsIncludeAttribute = true,
                            IsIncludeState = true,
                            BehaviorIds = new List<String>(){ behaviorId }
                        }
                    })[0];
                    if (behaviorInfo.Action == BehaviorActionNames.ImportLesson)
                    {
                        // 执行过即导入中
                        if (behaviorInfo.State.HasFlag(BehaviorState.IsExecuted))
                        {
                            result.State = ClassStateConstants.LessonImporting;
                        } 
                        else
                        {
                            result.State = ClassStateConstants.LessonUploading;
                            result.LessonInfo.LessonPackagePath = behaviorInfo.Attributes.ContainsKey(BehaviorAttributeNames.LessonPackagePath)
                                ? behaviorInfo.Attributes[BehaviorAttributeNames.LessonPackagePath] : String.Empty;
                        }
                    } 
                    else if (behaviorInfo.Action == BehaviorActionNames.TeacherScan)
                    {
                        result.State = ClassStateConstants.LessonSynching;
                    }
                    result.WorkBehaviorId = behaviorId;
                }
                else
                {
                    result.State = ClassStateConstants.InClass;
                }
            }
            else
            {
                result.State = ClassStateConstants.Unstart;
            }

            return result;
        }

        /// <summary>
        /// 上课
        /// </summary>
        /// <param name="classroomId"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public ClassStartResult BeginClass(String classroomId, ClassStartParam parameter)
        {
            var result = new ClassStartResult();
            var classroomWorkInfo = _classroom.GetClassroomWorkInfo(new Generic.ClassroomTeaching.Lesson.Param.ClassroomWorkInfoGetParam()
            {
                TeachingSpaceId = classroomId
            });
            if (classroomWorkInfo.IsOnClass)
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent((new { Code = Const.ErrorCodes.RepeatBeginClass }).ToJSONString())
                });
            }

            var behaviorCreateResult = _batchBehavior.Create(new List<BehaviorCreateParam>()
            {
                new BehaviorCreateParam()
                {
                    Action = BehaviorActionNames.BeginClass,
                    Principal = new BehaviorPrincipal()
                    {
                        Type = PrincipalType.MySelf
                    },
                    Reception = new BehaviorReception()
                    {
                        Data = classroomId,
                        Type = ReceptionType.Identify
                    },
                    Exclusive = new BehaviorExclusive()
                    {
                        ActionList = new List<String>(){ BehaviorActionNames.BeginClass },
                        ActionRange = ActionExclusiveRange.ActionList,
                        PrincipalRange = PrincipalExclusiveRange.Global
                    }
                }
            })[0];
            if (behaviorCreateResult.Success)
            {
                var clearBehavior = false;
                var errorCode = String.Empty;
                // 获取教学空间资产信息得到库标识
                var teachingSpaceFullInfo = _teachingSpace.GetTeachingSpace(classroomId);
                var libraryId = teachingSpaceFullInfo.Accessories.First(a => a.AccessoryType == TeachingSpaceAccessoryType.Library).AccessoryId;

                if (!String.IsNullOrWhiteSpace(libraryId))
                {
                    var lessonAddParam = new Generic.Classroom.LibraryBuilding.Param.LessonAddParam();
                    if (parameter.IsRelateCloud)
                    {
                        lessonAddParam.Name = parameter.CloudLessonInfo.CourseName;
                    }
                    else
                    {
                        lessonAddParam.Name = LessonConstants.DefaultCourseName;
                    }
                    var catalogIteIds = _lesson.AddLessons(libraryId, new List<Generic.Classroom.LibraryBuilding.Param.LessonAddParam>()
                    {
                        lessonAddParam
                    });
                    if (catalogIteIds.Any())
                    {
                        try
                        {
                            var username = "DEVICE000000001";
                            var profileInfoQueryResult = _limitQueryProfileByProperties.Query(new List<Dictionary<String, String[]>>()
                            {
                                new Dictionary<String, String[]>()
                                {
                                    {
                                        ProfileQueryConstants.UserName,
                                        new String[]{ username }
                                    }
                                }
                            });

                            // 创建课时
                            result.LessonId = _classroom.Start(new Generic.ClassroomTeaching.Lesson.Param.LessonStartParam()
                            {
                                TeachingSpaceId = classroomId,
                                IsRelateCloud = parameter.IsRelateCloud,
                                LessonName = parameter.IsRelateCloud ? parameter.CloudLessonInfo.LessonName : LessonConstants.DefaultLessonName,
                                LibraryCatalogItemId = catalogIteIds[0],
                                TeachingClassName = parameter.IsRelateCloud ? parameter.CloudLessonInfo.TeachingClassName : LessonConstants.DefaultTeachingClassName,
                                RelatedLesson = parameter.IsRelateCloud ? new Generic.ClassroomTeaching.Lesson.Struct.RelatedCloudLessonInfo()
                                {
                                    LessonId = parameter.CloudLessonInfo.CloudLessonId,
                                    CloudTeachingSpaceId = parameter.CloudLessonInfo.CloudTeachingSpaceId,
                                    ExportTime = parameter.CloudLessonInfo.ExportTime
                                } : null,
                                TeachToolId = profileInfoQueryResult.Items[0].SafetyId
                            });
                            // 创建名单
                            _census.Create(new List<CensusCreateParam>()
                            {
                                new CensusCreateParam()
                                {
                                    AuthorityId = "Default",
                                    CensusCode = result.LessonId,
                                    CensusTitle = parameter.IsRelateCloud ? $"{parameter.CloudLessonInfo.LessonName}学生名单" : $"{LessonConstants.DefaultLessonName}学生名单",
                                    DataDefinations = new List<DataDefinitionInfo>()
                                    {
                                        new DataDefinitionInfo(){ FieldName = StudentFieldNames.CorrelativeCode, FieldTitle = "用户相关码", FieldType = FieldType.UniqueCode, ValueType = Generic.Register.Census.Const.ValueType.String },
                                        new DataDefinitionInfo(){ FieldName = StudentFieldNames.Number, FieldTitle = "学号", FieldType = FieldType.ExtendData, ValueType = Generic.Register.Census.Const.ValueType.String },
                                        new DataDefinitionInfo(){ FieldName = StudentFieldNames.Name, FieldTitle = "姓名", FieldType = FieldType.ExtendData, ValueType = Generic.Register.Census.Const.ValueType.String },
                                        new DataDefinitionInfo(){ FieldName = StudentFieldNames.Portrait, FieldTitle = "头像", FieldType = FieldType.ExtendData, ValueType = Generic.Register.Census.Const.ValueType.String },
                                        new DataDefinitionInfo(){ FieldName = StudentFieldNames.Gender, FieldTitle = "性别", FieldType = FieldType.ExtendData, ValueType = Generic.Register.Census.Const.ValueType.String },
                                        new DataDefinitionInfo(){ FieldName = StudentFieldNames.LocalMemberId, FieldTitle = "本地成员标识", FieldType = FieldType.ExtendData, ValueType = Generic.Register.Census.Const.ValueType.String },
                                        new DataDefinitionInfo(){ FieldName = StudentFieldNames.LatestActiveTime, FieldTitle = "最近活跃时间", FieldType = FieldType.ExtendData, ValueType = Generic.Register.Census.Const.ValueType.String },
                                        new DataDefinitionInfo(){ FieldName = StudentFieldNames.IsSigned, FieldTitle = "是否签到", FieldType = FieldType.ExtendData, ValueType = Generic.Register.Census.Const.ValueType.String },
                                        new DataDefinitionInfo(){ FieldName = StudentFieldNames.IsLogon, FieldTitle = "是否登录", FieldType = FieldType.ExtendData, ValueType = Generic.Register.Census.Const.ValueType.String }
                                    },
                                    EnrollMethod = EnrollMethod.System,
                                    IsEnrollable = false,
                                    PurposeCode = "StudentCensus"
                                }
                            });
                            var lessonInfo = this.GetLessonInfo(result.LessonId);
                            var roleMemberIdsMap = this.GetGroupMemberIds(lessonInfo.RelativeGroupId, new List<String>() { Const.GroupRoleNames.Teacher });
                            var teacherIds = roleMemberIdsMap.ContainsKey(Const.GroupRoleNames.Teacher) ? roleMemberIdsMap[Const.GroupRoleNames.Teacher] : new List<String>() { profileInfoQueryResult.Items[0].SafetyId };

                            List<ActivityRecordInfo> activityRecordInfos = null;
                            activityRecordInfos = this.GetBeginLessonRecordInfos(Const.GroupRoleNames.Teacher, teacherIds);
                            if (activityRecordInfos.Any())
                            {
                                _lessonActivity.AddActivityRecord(result.LessonId, activityRecordInfos);
                            }
                            _message.SendMessage(new MessageData()
                            {
                                Content = MessageCodes.Clear,
                                IsRetained = true,
                                Quality = Fundamental.Message.Const.QualityOfServiceType.AtLeastOnce,
                                Topic = MessageCodes.All
                            });
                            clearBehavior = true;
                        }
                        catch (ErrorCodeException ex)
                        {
                            clearBehavior = true;
                            errorCode = ExceptionHelper.GetCodeByException(ex);
                        }
                    }
                    else
                    {
                        clearBehavior = true;
                    }
                }
                else
                {
                    clearBehavior = true;
                }
                if (clearBehavior)
                {
                    _behaviorCompletion.Complete(new List<BehaviorCompleteParam>()
                    {
                        new BehaviorCompleteParam()
                        {
                            BehaviorIds = new List<String>(){ behaviorCreateResult.BehaviorId },
                            ResultCode = "Complete",
                            ResultType = ResultType.UserDefine
                        }
                    });
                }
                if (!String.IsNullOrWhiteSpace(errorCode))
                {
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Content = new StringContent((new { Code = errorCode }).ToJSONString())
                    });
                }
            } 
            else if (!String.IsNullOrWhiteSpace(behaviorCreateResult.OccupiedBehaviorId))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent((new { Code = Const.ErrorCodes.BeginClassError }).ToJSONString())
                });
            }

            return result;
        }

        /// <summary>
        /// 下课
        /// </summary>
        /// <param name="classroomId"></param>
        /// <returns></returns>
        public void EndClass(String classroomId)
        {
            var classStopResult = _classroom.Stop(classroomId);
            if (classStopResult.StoppedLessonIds.Any())
            {
                List<ActivityRecordInfo> activityRecordInfos = null;
                activityRecordInfos = this.GetEndLessonRecordInfos(classStopResult.StoppedLessonIds[0]);
                _lessonActivity.AddActivityRecord(classStopResult.StoppedLessonIds[0], activityRecordInfos);
                var queryResult = _behaviorQuery.ComplexQuery(new List<QueryParameter>()
                {
                    new QueryParameter()
                    {
                        Conditions = new AndOperator()
                        {
                            OperatorExpression = new List<IConditionParameter>()
                            {
                                new EqualOperator { Name = Generic.Behavior.Single.Query.QueryName.ReceptionData, Value = classStopResult.StoppedLessonIds[0] },
                                new EqualOperator { Name = Generic.Behavior.Single.Query.QueryName.IsComplete, Value = "FALSE" }
                            }
                        }
                    }
                });
                if (queryResult.Count > 0 && queryResult[0].TotalCount > 0)
                {
                    var behaviorIds = queryResult[0].Results;
                    var behaviorInfos = _behaviorInfo.GetBehaviorInfo(new List<BehaviorInfoGetParam>()
                    {
                        new BehaviorInfoGetParam()
                        {
                            IsIncludeAction = true,
                            BehaviorIds = behaviorIds
                        }
                    });
                    var masterBehaviorIds = behaviorInfos.Where(b => BehaviorActionNames.CollaborationActions.Contains(b.Action)).Select(b => b.BehaviorId).ToList();
                    var singleBehaviorIds = behaviorInfos.Where(b => !BehaviorActionNames.CollaborationActions.Contains(b.Action)).Select(b => b.BehaviorId).ToList();
                    if (singleBehaviorIds.Any())
                    {
                        // 结束单一行为
                        _behaviorCompletion.Complete(new List<BehaviorCompleteParam>()
                        {
                            new BehaviorCompleteParam()
                            {
                                BehaviorIds = singleBehaviorIds,
                                ResultCode = "Complete",
                                ResultType = ResultType.UserDefine
                            }
                        });
                    }
                    if (masterBehaviorIds.Any())
                    {
                        // 结束协作行为
                        _collaborationCompletion.Complete(new List<CollaborationCompleteParam>()
                        {
                            new CollaborationCompleteParam()
                            {
                                SlaveBehaviorResult = new BehaviorCompleteData()
                                {
                                    ResultCode = "Complete",
                                    ResultType = ResultType.UserDefine
                                },
                                CollaborationResult = new BehaviorCompleteData()
                                {
                                    ResultCode = "Complete",
                                    ResultType = ResultType.UserDefine
                                },
                                MasterBehaviorResult = new BehaviorCompleteData()
                                {
                                    ResultCode = "Complete",
                                    ResultType = ResultType.UserDefine
                                },
                                BehaviorIds = masterBehaviorIds
                            }
                        });
                    }
                }
            }
            // 发送通知
            _message.SendMessage(new MessageData()
            {
                Content = MessageCodes.Over,
                IsRetained = true,
                Quality = Fundamental.Message.Const.QualityOfServiceType.AtLeastOnce,
                Topic = MessageCodes.All
            });
        }

        /// <summary>
        /// 开始课程导入
        /// </summary>
        /// <param name="lessonId"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public ImportStartResult StartImport(String lessonId, ImportStartParam parameter)
        {
            var result = new ImportStartResult();

            var behaviorCreateResult = _batchBehavior.Create(new List<BehaviorCreateParam>()
            {
                new BehaviorCreateParam()
                {
                    Action = BehaviorActionNames.ImportLesson,
                    Principal = new BehaviorPrincipal()
                    {
                        Type = PrincipalType.MySelf
                    },
                    Reception = new BehaviorReception()
                    {
                        Data = lessonId,
                        Type = ReceptionType.Identify
                    },
                    Exclusive = new BehaviorExclusive()
                    {
                        ActionList = new List<String>(){ BehaviorActionNames.ImportLesson, BehaviorActionNames.TeacherScan },
                        ActionRange = ActionExclusiveRange.ActionList,
                        PrincipalRange = PrincipalExclusiveRange.Global
                    },
                    Attributes = new Dictionary<String, String>()
                    {
                        { BehaviorAttributeNames.LessonPackagePath, parameter.LessonPackagePath },
                        { BehaviorAttributeNames.ClassroomId, parameter.ClassroomId }
                    }
                }
            })[0];
            if (behaviorCreateResult.Success)
            {
                result.WorkBehaviorId = behaviorCreateResult.BehaviorId;
            }
            else if (!String.IsNullOrWhiteSpace(behaviorCreateResult.OccupiedBehaviorId))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent((new { Code = Const.ErrorCodes.ImportLesson }).ToJSONString())
                });
            }

            return result;
        }

        /// <summary>
        /// 执行数据导入
        /// </summary>
        /// <param name="workBehaviorId"></param>
        /// <param name="handle"></param>
        public void ExecuteImport(String workBehaviorId, String handle)
        {
            // 检查句柄文件是否存在
            if (!_iOProvider.Exists(handle))
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent((new { Code = Const.ErrorCodes.LoseFile }).ToJSONString())
                });
            }
            var behaviorInfo = _behaviorInfo.GetBehaviorInfo(new List<BehaviorInfoGetParam>()
            {
                new BehaviorInfoGetParam()
                {
                    IsIncludeAction = true,
                    IsIncludeState = true,
                    IsIncludeReception = true,
                    IsIncludeAttribute = true,
                    BehaviorIds = new List<String>(){ workBehaviorId }
                }
            })[0];
            if (behaviorInfo.Action == BehaviorActionNames.ImportLesson && !behaviorInfo.State.HasFlag(BehaviorState.IsExecuted) 
                && behaviorInfo.Attributes.ContainsKey(BehaviorAttributeNames.ClassroomId))
            {
                CreateImportJob(behaviorInfo.Reception.Data, handle, workBehaviorId, behaviorInfo.Attributes[BehaviorAttributeNames.ClassroomId]);
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent((new { Code = Const.ErrorCodes.ExecuteImport }).ToJSONString())
                });
            }
        }

        /// <summary>
        /// 停止课程导入
        /// </summary>
        /// <param name="workBehaviorId"></param>
        public void StopImport(String workBehaviorId)
        {
            var behaviorInfo = _behaviorInfo.GetBehaviorInfo(new List<BehaviorInfoGetParam>()
            {
                new BehaviorInfoGetParam()
                {
                    IsIncludeAction = true,
                    BehaviorIds = new List<String>(){ workBehaviorId }
                }
            })[0];
            if (behaviorInfo.Action == BehaviorActionNames.ImportLesson)
            {
                _behaviorCompletion.Complete(new List<BehaviorCompleteParam>()
                {
                    new BehaviorCompleteParam()
                    {
                        BehaviorIds = new List<String>(){ workBehaviorId },
                        ResultCode = "Complete",
                        ResultType = ResultType.UserDefine
                    }
                });
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent((new { Code = Const.ErrorCodes.StopImport }).ToJSONString())
                });
            }
        }

        /// <summary>
        /// 获取教室二维码
        /// </summary>
        /// <param name="classroomId"></param>
        /// <returns></returns>
        public QRCodeInfo GetQRCodeInfo(String classroomId)
        {
            var section = _configSource.GetSection<InteractionConfigSection>(InteractionConfigSection.DefaultSectionName);
            var environmentConfig = _configSource.GetSection<EnvironmentVariableTable>(EnvironmentVariableTable.DefaultSectionName);
            var result = new QRCodeInfo()
            {
                C = classroomId,
                S = section.WifiSSID,
                P = section.WifiPassword,
                U = environmentConfig.ContainsKey("FrontProxyServer") ? environmentConfig["FrontProxyServer"] : String.Empty,
                E = section.Encryption,
                M = section.WorkMode
            };
            return result;
        }

        /// <summary>
        /// 学生注册并登录
        /// </summary>
        /// <param name="parameter"></param>
        public void LoginStudentDevice(String classroomId, String userDeviceCode)
        {
            // 使用设备码登录，若登录失败，则为其创建账号，并绑定该设备码
            var verfiyFlag = false;
            try
            {
                var verifyResult = _deviceLogon.Verify(new DeviceLogonParameter()
                {
                    DeviceId = userDeviceCode,
                    IP = HttpRequestExtension.GetRemoteAddress()
                });
                AvatarAuthContextExtensions.RecordAuthentication(verifyResult.Token);
            }
            catch (Exception)
            {
                verfiyFlag = true;
            }

            if (verfiyFlag)
            {
                // 为学生创建账号
                ProfileInfoWithSafetyId profileInfoWithSafetyId = null;
                var uniqueCode = _uniqueCode.Generate("GLOBAL");
                var userName = uniqueCode.ToString().PadLeft(9, '0');

                profileInfoWithSafetyId = _profile.Create(new RegistUserData()
                {
                    DisplayName = String.Empty,
                    Gender = Generic.Sociality.User.Struct.Gender.Unknown,
                    Password = userName,
                    Portrait = String.Empty,
                    TrueName = String.Empty,
                    UserName = userName,
                });

                // 登录学生账号
                var logonInfo = _passwordLogon.Verify(new PasswordLogonParameter()
                {
                    IP = HttpRequestExtension.GetRemoteAddress(),
                    LogonName = userName,
                    Password = userName
                });

                AvatarAuthContextExtensions.RecordAuthentication(logonInfo.Token);
                // 绑定账号与设备码
                _batchUserDevice.BindUserDeviceId(new Dictionary<String, String>()
                {
                    { profileInfoWithSafetyId.SafetyId, userDeviceCode }
                });
            }
        }
        
        /// <summary>
        /// 加入教室
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public StudentAttendClassResult AttendClass(String classroomId, StudentAttendClassParam parameter)
        {
            var result = new StudentAttendClassResult();
            var classroomWorkInfo = _classroom.GetClassroomWorkInfo(new Generic.ClassroomTeaching.Lesson.Param.ClassroomWorkInfoGetParam()
            {
                TeachingSpaceId = classroomId
            });
            if (classroomWorkInfo.IsOnClass)
            {
                var memberId = _lessonMember.JoinLesson(new Generic.ClassroomTeaching.Lesson.Param.LessonJoinParam()
                {
                    Role = MemberRoleNames.Student,
                    LessonId = classroomWorkInfo.ActiveLesson.LessonId
                });
                var censusId = this.GetCensusByLessonId(classroomWorkInfo.ActiveLesson.LessonId);

                if (!String.IsNullOrWhiteSpace(parameter.PortraitId))
                {
                    var pictureSetImportParam = new PictureSetImportParam()
                    {
                        PictureSets = new List<Static.Picture.WebAPI.Struct.FileData>()
                    {
                        new Static.Picture.WebAPI.Struct.FileData()
                        {
                            Data = parameter.PortraitFile
                        }
                    }
                    };
                    _pictureSet.Import(pictureSetImportParam);
                }

                var memberInfo = new StudentFullInfo()
                {
                    Gender = ConvertEnumFromString(parameter.Gender),
                    Name = parameter.DisplayName,
                    Portrait = parameter.PortraitId,
                    CorrelativeCode = parameter.CorrelativeCode,
                    MemberId = memberId,
                    IsLogon = "true",
                    IsSigned = "false",
                    LatestActiveTime = _timeProvider.GetNow().ToString("O")
                };
                this.UpdateCensus(censusId, new List<StudentFullInfo>() { memberInfo });

                var userIndividualInfoMap = _enrolledIndividual.Get(new List<EnrolledIndividualGetParam>()
                {
                    new EnrolledIndividualGetParam()
                    {
                        CensusIds = new List<String>(){ censusId },
                        FieldNames = new List<String>(){ StudentFieldNames.CorrelativeCode },
                        UserIds = new List<String>(){ Registrant.Myself }
                    }
                })[0].UserIndividualInfoMap;
                if (!userIndividualInfoMap.Any())
                {
                    _enrollment.Add(new List<EnrollmentAddParam>()
                    {
                        new EnrollmentAddParam()
                        {
                            CensusId = censusId,
                            EnrollmentsInfos =new List<EnrollmentInfo>()
                            {
                                new EnrollmentInfo()
                                {
                                    UniqueCode = parameter.CorrelativeCode,
                                    UserId = memberId
                                }
                            }
                        }
                    });
                    _lessonAttendance.AddAttendanceRecord(classroomWorkInfo.ActiveLesson.LessonId, new List<MemberAttendance>()
                    {
                        new MemberAttendance()
                        {
                            MemberId = memberId,
                            RegisterTime = _timeProvider.GetNow(),
                            Status = AttendanceStatus.Participation
                        }
                    });

                    List<ActivityRecordInfo> activityRecordInfos = null;
                    activityRecordInfos = this.GetBeginLessonRecordInfos(Const.GroupRoleNames.Student, new List<String> { memberId });
                    _lessonActivity.AddActivityRecord(classroomWorkInfo.ActiveLesson.LessonId, activityRecordInfos);
                }
                result.LessonId = classroomWorkInfo.ActiveLesson.LessonId;
                result.LessonName = classroomWorkInfo.ActiveLesson.Name;
                result.CourseName = classroomWorkInfo.ActiveLesson.TeachingClassName;
                result.StartTime = classroomWorkInfo.ActiveLesson.StartTime;
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent((new { Code = Const.ErrorCodes.LessonNoteOpen }).ToJSONString())
                });
            }

            return result;
        }

        /// <summary>
        /// 总览学生
        /// </summary>
        /// <param name="classroomId"></param>
        /// <returns></returns>
        public StudentOverseeResult OverseeStudent(String classroomId)
        {
            var result = new StudentOverseeResult();
            var classroomWorkInfo = _classroom.GetClassroomWorkInfo(new Generic.ClassroomTeaching.Lesson.Param.ClassroomWorkInfoGetParam()
            {
                TeachingSpaceId = classroomId
            });
            result.IsOnClass = classroomWorkInfo.IsOnClass;

            if (classroomWorkInfo.IsOnClass)
            {
                var censusId = this.GetCensusByLessonId(classroomWorkInfo.ActiveLesson.LessonId);
                result.LessonMoment.lessonId = classroomWorkInfo.ActiveLesson.LessonId;
                result.LessonMoment.NameListId = censusId;
                result.LessonMoment.TeachingClassName = classroomWorkInfo.ActiveLesson.TeachingClassName;
                // 复合查询action为签到的行为
                var behaviorQueryResults = _behaviorQuery.ComplexQuery(new List<QueryParameter>()
                {
                    new QueryParameter()
                    {
                        Conditions = new AndOperator()
                        {
                            OperatorExpression = new List<IConditionParameter>()
                            {
                                new EqualOperator { Name = Generic.Behavior.Single.Query.QueryName.Action, Value = BehaviorActionNames.StudentSign },
                                new EqualOperator { Name = Generic.Behavior.Single.Query.QueryName.ReceptionData, Value = classroomWorkInfo.ActiveLesson.LessonId },
                                new EqualOperator { Name = Generic.Behavior.Single.Query.QueryName.IsComplete, Value = "FALSE" }
                            }
                        }
                    }
                });
                if(behaviorQueryResults.Any() && behaviorQueryResults[0].TotalCount > 0)
                {
                    var behaviorId = behaviorQueryResults[0].Results[0];
                    var behaviorInfo = _behaviorInfo.GetBehaviorInfo(new List<BehaviorInfoGetParam>()
                    {
                        new BehaviorInfoGetParam()
                        {
                            IsIncludeAction = true,
                            IsIncludeTime = true,
                            BehaviorIds = new List<String>(){ behaviorId }
                        }
                    })[0];
                    if (behaviorInfo.Action == BehaviorActionNames.StudentSign)
                    {
                        var endTime = behaviorInfo.Time.CreateTime.AddMinutes(1);
                        var now = _timeProvider.GetNow();
                        result.LessonMoment.SignInfo.IsSigning = true;
                        result.LessonMoment.SignInfo.SignSessonId = behaviorId;
                        result.LessonMoment.SignInfo.SignDuringSeconds = Convert.ToInt32(endTime.Subtract(now).TotalSeconds);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 查询学生列表
        /// </summary>
        /// <param name="nameListId"></param>
        /// <returns></returns>
        public List<Struct.StudentInfo> QueryStudents(String nameListId)
        {
            var result = new List<Struct.StudentInfo>();
            var individualInfos = _individual.Get(new List<IndividualGetParam>()
            {
                new IndividualGetParam()
                {
                    CensusIds = new List<String>() { nameListId },
                    IsGetAllIndividuals = true
                }
            });
            if (individualInfos.Any() && individualInfos[0].Individuals.Any())
            {
                var section = _configSource.GetSection<InteractionConfigSection>(InteractionConfigSection.DefaultSectionName);
                result = individualInfos[0].Individuals.Select(i => new Struct.StudentInfo()
                {
                    StudentId = i.IndividualDataset[StudentFieldNames.LocalMemberId],
                    IsSigned = Convert.ToBoolean(i.IndividualDataset[StudentFieldNames.IsSigned]),
                    IsLogon = Convert.ToBoolean(i.IndividualDataset[StudentFieldNames.IsLogon]),
                    LatestActiveTime = String.IsNullOrEmpty(i.IndividualDataset[StudentFieldNames.LatestActiveTime]) ? null
                    : new DateTime?(DateTime.ParseExact(i.IndividualDataset[StudentFieldNames.LatestActiveTime], "O", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal)),
                    Name = i.IndividualDataset[StudentFieldNames.Name],
                    Number = i.IndividualDataset.ContainsKey(StudentFieldNames.Number) ? i.IndividualDataset[StudentFieldNames.Number] : String.Empty,
                    Portrait = i.IndividualDataset.ContainsKey(StudentFieldNames.Number) ? i.IndividualDataset[StudentFieldNames.Portrait] : section.DefaultStudentPortrait
                }).ToList();
            }

            return result;
        }
        
        /// <summary>
        /// 开始签到
        /// </summary>
        /// <param name="classroomId"></param>
        /// <returns></returns>
        public SignBeginResult BeginSign(String classroomId)
        {
            var result = new SignBeginResult();
            var classroomWorkInfo = _classroom.GetClassroomWorkInfo(new Generic.ClassroomTeaching.Lesson.Param.ClassroomWorkInfoGetParam()
            {
                TeachingSpaceId = classroomId
            });
            if (classroomWorkInfo.IsOnClass)
            {
                var lessonInfos = _lessonInfo.GetLessonInfo(new Generic.TeachingSpace.Param.LessonInfoGetParam()
                {
                    SpaceId = classroomId,
                    LessonIds = new List<String>() { classroomWorkInfo.ActiveLesson.LessonId }
                });
                if (lessonInfos.Any())
                {
                    var interactionActions = BehaviorActionNames.InteractiveAction;
                    // 创建主行为
                    var masterBehaviorCreateResult = _batchBehavior.Create(new List<BehaviorCreateParam>()
                    {
                        new BehaviorCreateParam()
                        {
                            Action = BehaviorActionNames.StudentSign,
                            Exclusive = new BehaviorExclusive()
                            {
                                ActionList = interactionActions,
                                ActionRange = ActionExclusiveRange.ActionList,
                                PrincipalRange = PrincipalExclusiveRange.Global
                            },
                            Principal = new BehaviorPrincipal()
                            {
                                Type = PrincipalType.MySelf
                            },
                            Reception = new BehaviorReception()
                            {
                                Data = classroomWorkInfo.ActiveLesson.LessonId,
                                Type = ReceptionType.Identify
                            }
                        }
                    })[0];
                    if (masterBehaviorCreateResult.Success)
                    {
                        result.SignSessonId = masterBehaviorCreateResult.BehaviorId;
                        var groupId = lessonInfos[0].RelativeGroupId;
                        var getRoleSetPathResult = _getRoleSetPath.Get(new List<GetRoleSetParameter>()
                        {
                            new GetRoleSetParameter()
                            {
                                GroupId = groupId,
                                RoleSetName = GroupRoleSetNames.Teaching
                            }
                        })[0];

                        // 创建协作行为
                        var collaborationCreateParam = new CollaborationCreateParam()
                        {
                            AcceptableParticipants = new List<BehaviorPrincipal>()
                        {
                            new BehaviorPrincipal()
                            {
                                Data = getRoleSetPathResult.RankPath[GroupRanks.Student],
                                Type = PrincipalType.ManyUser
                            }
                        },
                            BehaviorIds = new List<String>() { masterBehaviorCreateResult.BehaviorId },
                            SlaveCreateData = new CollaborationSlaveCreateData()
                            {
                                DataCode = BehaviorActionNames.StudentSign,
                                ReuseExistDataCode = true,
                                SlaveBehaviorCreateData = new BehaviorCreateData()
                                {
                                    Action = BehaviorActionNames.StudentSign,
                                    Reception = null
                                }
                            },
                            UserData = new CollaborationUserData()
                            {
                                GroupTag = BehaviorActionNames.StudentSign,
                                ProjectId = classroomWorkInfo.ActiveLesson.LessonId,
                            }
                        };
                        _batchCollaboration.Create(new List<CollaborationCreateParam>()
                        {
                            collaborationCreateParam
                        });
                    } 
                    else if (!String.IsNullOrWhiteSpace(masterBehaviorCreateResult.OccupiedBehaviorId))
                    {
                        result.IsSuccess = false;
                        result.ErrorCode = Const.ErrorCodes.ExclusiveBehaviorExist;
                        result.ErrorData = masterBehaviorCreateResult.OccupiedAction;
                    }
                    else
                    {
                        throw new Exception("Behavior Create Error");
                    }
                }
            }
            else
            {
                throw new Exception("Class Is Not Begin");
            }

            return result;
        }

        /// <summary>
        /// 停止签到
        /// </summary>
        /// <param name="signSessonId"></param>
        public void StopSign(String signSessonId)
        {
            var behaviorInfo = _behaviorInfo.GetBehaviorInfo(new List<BehaviorInfoGetParam>()
            {
                new BehaviorInfoGetParam()
                {
                    IsIncludeAction = true,
                    BehaviorIds = new List<String>(){ signSessonId }
                }
            })[0];
            if (behaviorInfo.Action == BehaviorActionNames.StudentSign)
            {
                _collaborationCompletion.Complete(new List<CollaborationCompleteParam>()
                {
                    new CollaborationCompleteParam()
                    {
                        SlaveBehaviorResult = new BehaviorCompleteData()
                        {
                            ResultCode = "Complete",
                            ResultType = ResultType.UserDefine
                        },
                        CollaborationResult = new BehaviorCompleteData()
                        {
                            ResultCode = "Complete",
                            ResultType = ResultType.UserDefine
                        },
                        MasterBehaviorResult = new BehaviorCompleteData()
                        {
                            ResultCode = "Complete",
                            ResultType = ResultType.UserDefine
                        },
                        BehaviorIds = new List<String>(){ signSessonId }
                    }
                });
            }
            else
            {
                throw new Exception("Stop Sign Error");
            }
        }

        private List<ActivityRecordInfo> GetBeginLessonRecordInfos(String role, List<String> memberIds)
        {
            var activityRecordInfos = new List<ActivityRecordInfo>();
            if (memberIds.Count > 0)
            {
                activityRecordInfos.Add(new ActivityRecordInfo()
                {
                    ActivityTitle = "开始上课",
                    Category = "DEFAULT",
                    Detail = new ActivityRecordDetail()
                    {
                        DetailData = "开始上课",
                        DetailType = DetailType.Simple
                    },
                    IconName = "CLASS",
                    MemberIds = memberIds,
                    UserRole = role
                });
            }
            return activityRecordInfos;
        }

        private List<ActivityRecordInfo> GetEndLessonRecordInfos(String lessonId)
        {
            // 通过名单获取已出勤人数
            var statisticsInfo = this.GetStatisticsInfo(lessonId);

            var activityRecordInfos = new List<ActivityRecordInfo>();

            var lessonInfo = this.GetLessonInfo(lessonId);
            var roleMemberIdsMap = this.GetGroupMemberIds(lessonInfo.RelativeGroupId, new List<String>() { Const.GroupRoleNames.Teacher, Const.GroupRoleNames.Student });
            var teacherIds = roleMemberIdsMap.ContainsKey(Const.GroupRoleNames.Teacher) ? roleMemberIdsMap[Const.GroupRoleNames.Teacher] : new List<String>();
            if (!teacherIds.Any())
            {
                var username = "DEVICE000000001";
                var profileInfoQueryResult = _limitQueryProfileByProperties.Query(new List<Dictionary<String, String[]>>()
                {
                    new Dictionary<String, String[]>()
                    {
                        {
                            ProfileQueryConstants.UserName,
                            new String[]{ username }
                        }
                    }
                });
                teacherIds.Add(profileInfoQueryResult.Items[0].SafetyId);
            }
            // 添加教师记录
            activityRecordInfos.Add(new ActivityRecordInfo()
            {
                ActivityTitle = "下课",
                Category = "DEFAULT",
                Detail = new ActivityRecordDetail()
                {
                    DetailData = new List<ContentComplexInfo>()
                {
                    new ContentComplexInfo()
                    {
                        MainItem = new MainItemInfo()
                        {
                            Description = "出勤",
                            Name = statisticsInfo.JoinedCount.ToString()
                        },
                        SubItem = new SubItemInfo()
                        {
                            Description = "总人数",
                            Name = statisticsInfo.TotalCount.ToString()
                        }
                    }
                }.ToJSONString(),
                    DetailType = DetailType.Complex
                },
                IconName = "CLASS",
                MemberIds = teacherIds,
                UserRole = Const.GroupRoleNames.Teacher
            });

            var joinedStudentIds = roleMemberIdsMap.ContainsKey(Const.GroupRoleNames.Student) ? roleMemberIdsMap[Const.GroupRoleNames.Student] : new List<String>();
            if (joinedStudentIds.Count > 0)
            {
                // 添加学生记录
                activityRecordInfos.Add(new ActivityRecordInfo()
                {
                    ActivityTitle = "下课",
                    Category = "DEFAULT",
                    Detail = new ActivityRecordDetail()
                    {
                        DetailData = "下课",
                        DetailType = DetailType.Simple
                    },
                    IconName = "CLASS",
                    MemberIds = joinedStudentIds,
                    UserRole = Const.GroupRoleNames.Student
                });
            }
            return activityRecordInfos;
        }

        private Dictionary<String, List<String>> GetGroupMemberIds(String groupId, List<String> groupRoles)
        {
            var roleMemberIdsMap = new Dictionary<String, List<String>>();
            if (!String.IsNullOrWhiteSpace(groupId))
            {
                var queryResult = _queryUserMemberRole.Query(new QueryMemberRoleParameter()
                {
                    Condition = new AndOperator()
                    {
                        OperatorExpression = new List<IConditionParameter>()
                        {
                            new EqualOperator(){ Name = Generic.Sociality.Group.Const.QueryName.GroupId, Value = groupId },
                            new InSetOperator(){ Name = Generic.Sociality.Group.Const.QueryName.RoleRank, Value = groupRoles.Select(r => GroupRanks.ConvertRankFromRoleName(r).ToString()).ToList() }
                        }
                    }
                });
                if (queryResult.TotalCount > 0)
                {
                    roleMemberIdsMap = queryResult.Results.GroupBy(r => r.RoleRank, r => r.MemberId).ToDictionary(g => Const.GroupRoleNames.ConvertRoleNameFromRank(g.Key), g => g.ToList());
                }
            }
            return roleMemberIdsMap;
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

        private String GetCensusByLessonId(String lessonId)
        {
            var queryParam = new AndOperator()
            {
                OperatorExpression = new List<IConditionParameter>()
                {
                    new  EqualOperator(){ Name = Generic.Register.Census.Query.QueryName.CensusCode, Value = lessonId }
                }
            };
            var queryResult = _censusQuery.Query(new List<QueryParameter>()
            {
                new QueryParameter()
                {
                    Conditions = queryParam
                }
            })[0];
            return queryResult.Results[0];
        }

        private Callback.Struct.Gender ConvertEnumFromString(String value)
        {
            var gender = Callback.Struct.Gender.Unknown;
            switch (value)
            {
                case Const.GenderConstants.UnknownCode: gender = Callback.Struct.Gender.Unknown; break;
                case Const.GenderConstants.MaleCode: gender = Callback.Struct.Gender.Male; break;
                case Const.GenderConstants.FemaleCode: gender = Callback.Struct.Gender.Female; break;
            }
            return gender;
        }

        private void UpdateCensus(String censusId, List<StudentFullInfo> memberSummaryInfos)
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
                    CensusIds = new List<String>(){ censusId },
                    UpdateParams = individualUpdateItems,
                }
            });
        }

        private Struct.MemberStatisticsInfo GetStatisticsInfo(String lessonId)
        {
            if (String.IsNullOrWhiteSpace(lessonId))
            {
                throw new ArgumentException("parameter is not valid");
            }

            // 根据教学场景标识获取群标识
            var censusId = this.GetCensusByLessonId(lessonId);

            var memberStatisticsInfo = new Struct.MemberStatisticsInfo();
            if (!String.IsNullOrWhiteSpace(censusId))
            {
                // 根据名单标识，查询名单下学生数量，得到总人数和已注册人数
                var queryResults = _individualQuery.Query(new List<QueryParameter>()
                {
                    new QueryParameter()
                    {
                        Conditions = new AndOperator()
                        {
                            OperatorExpression = new List<IConditionParameter>()
                            {
                                new EqualOperator() { Name = Generic.Register.Census.Query.QueryName.CensusId, Value = censusId }
                            }
                        },
                        Page = new PageParameter()
                        {
                            IsCountOnly = true
                        }
                    },
                    new QueryParameter()
                    {
                        Conditions = new AndOperator()
                        {
                            OperatorExpression = new List<IConditionParameter>()
                            {
                                new EqualOperator() { Name = Generic.Register.Census.Query.QueryName.CensusId, Value = censusId },
                                new EqualOperator() { Name = Generic.Register.Census.Query.QueryName.Enrolled, Value = "TRUE" }
                            }
                        },
                        Page = new PageParameter()
                        {
                            IsCountOnly = true
                        }
                    }
                });
                if (queryResults != null)
                {
                    memberStatisticsInfo.TotalCount = queryResults[0].TotalCount;
                    memberStatisticsInfo.JoinedCount = queryResults[1].TotalCount;
                }
            }
            return memberStatisticsInfo;
        }
    }
}