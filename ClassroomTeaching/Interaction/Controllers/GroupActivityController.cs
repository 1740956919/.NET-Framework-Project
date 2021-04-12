using LINDGE.PARA.Fundamental.Avatar.Session.SDK;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Service.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct;
using System.Linq;
using System.Web.Http;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Controllers
{
    [AvatarAuthorize]
    public class GroupActivityController : ApiController
    {
        private readonly IMark _mark = null;
        private readonly IProjection _projection = null;
        private readonly IBroadcast _broadcast = null;
        private readonly ITeachingScene _teachingScene = null;
        private readonly IDiscussControl _discussControl = null;
        private readonly IMonitor _monitor = null;
        private readonly IEnvironment _environment = null;

        public GroupActivityController(IMark markService,
            IProjection projectionService,
            IBroadcast broadcastService,
            ITeachingScene teachingSceneService,
            IDiscussControl discussControlService,
            IMonitor monitorService,
            IEnvironment environmentService)
        {
            _mark = markService;
            _projection = projectionService;
            _broadcast = broadcastService;
            _teachingScene = teachingSceneService;
            _discussControl = discussControlService;
            _monitor = monitorService;
            _environment = environmentService;
        }

        [HttpGet]
        public object GetLessonActivity(string id)
        {
            var markCheckResult = _mark.Check(id);
            var projectCheckResult = _projection.Check(id);

            return new
            {
                IsMarking = markCheckResult.IsMarking,
                IsReceiveScreenProjection = projectCheckResult.IsProject
            };
        }

        [HttpPost]
        public GroupActivityInfo GetGroupActivity()
        {
            var sceneInfo = _teachingScene.Query(false)[0];

            var lessonId = sceneInfo.CurrentLesson.LessonId;

            var discussCheckResult = _discussControl.Check(lessonId);

            var isReceiveBroadcast = false;
            var boradcastChannelId = -1;
            var localDeviceId = _environment.GetMyDeviceId();
            var broadcastCheckResult = _broadcast.Check(lessonId);
            if (broadcastCheckResult.IsBroadcast && broadcastCheckResult.DeviceIds.Contains(localDeviceId))
            {
                isReceiveBroadcast = true;;
                var channelDeviceInfos = _broadcast.Accept(lessonId);
                boradcastChannelId = channelDeviceInfos[0].ChannelId;
            }

            if (isReceiveBroadcast)
            {
                var monitorCheckResult = _monitor.Check(lessonId);
                if (monitorCheckResult.IsMonitor)
                {
                    var monitorLockDeviceIds = _monitor.GetLockDevice(monitorCheckResult.MonitorId);
                    if (monitorLockDeviceIds.Contains(localDeviceId))
                    {
                        isReceiveBroadcast = false;
                    }
                }
            }

            return new GroupActivityInfo()
            {
                SceneInfo = new SceneLessonInfo()
                {
                    HasScene = true,
                    IsActive = sceneInfo.IsActive,
                    LessonId = sceneInfo.CurrentLesson.LessonId,
                    Name = sceneInfo.SceneName,
                    SceneId = sceneInfo.SceneId
                },
                IsReceiveBroadcast = isReceiveBroadcast,
                BroadcastChannelId = boradcastChannelId,
                DiscussionInfo = new DiscussionInfo()
                {
                    DiscussionId = discussCheckResult.DiscussId,
                    Type = discussCheckResult.Type,
                    State = discussCheckResult.State
                },
                IsDiscussing = discussCheckResult.IsDiscussing,
            };
        }
    }
}
