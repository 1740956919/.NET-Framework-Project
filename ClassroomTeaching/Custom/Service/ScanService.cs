using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Param;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Service.Interface;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Struct;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Logic;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Result;
using LINDGE.PARA.Translayer.Static.Picture.WebAPI;
using LINDGE.PARA.Translayer.Static.Picture.WebAPI.Param;
using LINDGE.PARA.Translayer.Static.Picture.WebAPI.Result;
using LINDGE.Proxy;
using LINDGE.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Service
{
    public class ScanService : IScanService
    {
        private readonly IMember _member = null;
        private readonly ITeachingScene _teachingScene = null;
        private readonly ILesson _lesson = null;
        private readonly IConfigSource _configSource = null;
        private readonly IPictureSet _pictureSet = null;

        public ScanService(IMember member,
            ITeachingScene teachingScene,
            ILesson lesson,
            IConfigSource configSource,
            IProxy<IPictureSet> pictureSetProxy)
        {
            _member = member;
            _teachingScene = teachingScene;
            _lesson = lesson;
            _configSource = configSource;
            _pictureSet = pictureSetProxy.GetObject();
        }

        /// <summary>
        /// 加入教室上课
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public StudentAttendClassResult AttendClass(StudentAttendClassParam parameter)
        {
            var result = new StudentAttendClassResult();
            var section = _configSource.GetSection<CustomConfigSection>(CustomConfigSection.DefaultSectionName);
            var lessonInfo = _lesson.Get(parameter.LessonId);
            result.StartTime = lessonInfo.BeginTime;
            result.LessonName = String.IsNullOrWhiteSpace(lessonInfo.LessonName) ? section.DefaultLessonName : lessonInfo.LessonName;
            result.CourseName = String.IsNullOrWhiteSpace(lessonInfo.TeachingClassName) ? section.DefaultClassName : lessonInfo.TeachingClassName;
            // 上传图片
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
            // 学生加入教学场景
            _member.StudentJoin(new StudentJoinLessonParam()
            {
                LessonId = parameter.LessonId,
                StudentJoinInfo = new StudentJoinInfo()
                {
                    Gender = ConvertGender.ConvertEnumFromString(parameter.Gender),
                    Name = parameter.DisplayName,
                    Portrait = parameter.PortraitId,
                    CorrelativeCode = parameter.CorrelativeCode,
                    DeviceId = parameter.UserDeviceCode
                }
            });
            return result;
        }

        /// <summary>
        /// 下载课时并上课
        /// </summary>
        /// <param name="parameter"></param>
        public void DownLoadLessonAndAttendClass(TeacherAttendClassParam parameter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 学生注册并登录
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public StudentLoginResult LoginStudentDevice(StudentLoginParam parameter)
        {
            var result = new StudentLoginResult();
            // 查询活跃的教学场景
            var sceneInfos = _teachingScene.Query(true);
            if (sceneInfos.Any() && sceneInfos[0].IsActive)
            {
                result.LessonId = sceneInfos[0].CurrentLesson.LessonId;
                _member.MemberRegister(parameter.UserDeviceCode);
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent((new { Code = ErrorCodes.WaitStart }).ToJSONString())
                });
            }

            return result;
        }

        /// <summary>
        /// 获取所有可以下载的课时列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public LessonQueryResult QueryAllDownLoadLessons(Param.LessonQueryParam parameter)
        {
            throw new NotImplementedException();
        }
    }
}