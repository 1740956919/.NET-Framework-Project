using System;
using System.Linq;
using System.Web.Http;

using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Service.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Controllers
{
    public class LessonActivityController : ApiController
    {
        private readonly ILesson _lesson = null;

        public LessonActivityController(ILesson lessonService)
        {
            _lesson = lessonService;
        }

        /// <summary>
        /// 获取场景状态
        /// </summary>
        /// <param name="id">课时标识</param>
        /// <returns></returns>
        [AvatarAuthorize]
        [HttpGet]
        public LessonActivityInfo GetLessonActivity(String id)
        {
            var lessonState = _lesson.GetState(id);
            return new LessonActivityInfo()
            {
                IsGroupDiscussing = lessonState.IsGroupDiscussing,
                IsInteracting = lessonState.IsGrabAnswering || lessonState.IsPracticing,
                IsMarking = lessonState.IsMaring,
                IsScreening = lessonState.IsBroadcasting || lessonState.IsMonitoring || lessonState.IsProjecting,
                IsTeaching = lessonState.IsActive,
                IsGrouping = lessonState.IsGrouping
            };
        }
    }
}
