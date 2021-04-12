using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Service.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class TeachingLessonController : ApiController
    {
        private readonly ITeachingScene _teachingScene = null;
        private readonly ILesson _lesson = null;
        private readonly IConfigSource _configSource = null;

        public TeachingLessonController(ITeachingScene teachingScene,
            ILesson lessonService,
            IConfigSource configSource)
        {
            _teachingScene = teachingScene;
            _lesson = lessonService;
            _configSource = configSource;
        }

        [HttpPut]
        public object Create()
        {
            var section = _configSource.GetSection<InteractionConfigSection>(InteractionConfigSection.DefaultSectionName);
            var sceneInfo = _teachingScene.Create(new List<String>() { section.DefaultClassName })[0];

            return new
            {
                sceneInfo.SceneId,
                sceneInfo.CurrentLesson.LessonId
            };
        }

        [HttpDelete]
        public void Stop(String id)
        {
            _lesson.Stop(id);
        }

        [HttpPost]
        public void Start(String id, String receptor)
        {
            _lesson.Start(id, receptor);
        }

        [HttpGet]
        public SceneLessonInfo Query()
        {
            var result = new SceneLessonInfo();
            var sceneInfos = _teachingScene.Query();
            if (sceneInfos.Any())
            {
                var sceneInfo = sceneInfos[0];
                result.HasScene = true;
                result.IsActive = sceneInfo.IsActive;
                result.Name = sceneInfo.SceneName;
                result.SceneId = sceneInfo.SceneId;
                result.LessonId = sceneInfo.CurrentLesson.LessonId;
            }
            return result;
        }
    }
}
