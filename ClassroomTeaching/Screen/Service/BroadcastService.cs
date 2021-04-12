using LINDGE.PARA.Generic.Behavior.Single.API.Basic;
using LINDGE.PARA.Generic.Behavior.Single.Param;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Const;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Service.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Struct;
using LINDGE.Proxy;
using LINDGE.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Service
{
    public class BroadcastService : IBroadcastService
    {
        private readonly ILesson _lesson = null;
        private readonly IMonitor _monitor = null;
        private readonly IBroadcast _broadcast = null;
        private readonly IProjection _projection = null;
        private readonly IEnvironment _environment = null;

        public BroadcastService(
            ILesson lessonService,
            IMonitor monitorService,
            IBroadcast broadcastService,
            IProjection projectionService,
            IEnvironment environmentService
            )
        {
            _lesson = lessonService;
            _monitor = monitorService;
            _broadcast = broadcastService;
            _projection = projectionService;
            _environment = environmentService;
        }

        /// <summary>
        /// 查询屏幕广播状态
        /// </summary>
        /// <param name="lessionId">课时标识</param>
        /// <returns></returns>
        public WorkStateInfo GetBroadcastWorkState(string lessionId)
        {
            var broadcastCheckResult = _broadcast.Check(lessionId);
            var deviceIds = broadcastCheckResult.DeviceIds;
            var monitorCheckResult = _monitor.Check(lessionId);
            var projectionCheckResult = _projection.Check(lessionId);

            //小组屏设备信息
            var deviceInfos = _environment.GetAllAccepter(lessionId);
            var groupDeviceInfos = deviceInfos.Where(d => d.Type == DeviceTypes.GroupScreen).Select(d => new DeviceInfo() {
                DeviceId = d.Id,
                IsRecevice = deviceIds.Contains(d.Id),
                DeviceName = d.Name
            }).ToList();

            return new WorkStateInfo()
            {
                IsBroadcast = broadcastCheckResult.IsBroadcast,
                BroadcastId = broadcastCheckResult.BroadcastId,
                GroupScreens = groupDeviceInfos,
                IsMonitor = monitorCheckResult.IsMonitor,
                MonitorId = monitorCheckResult.MonitorId,
                IsProjection = projectionCheckResult.IsProject,
                ProjectionId = projectionCheckResult.ProjectionId
            };
        }

        /// <summary>
        /// 小组屏接收广播
        /// </summary>
        /// <param name="sceneId">场景标识</param>
        /// <returns></returns>
        public BroadcastReceiveResult ReceiveBroadcast(string lessionId)
        {          
            var sceneInfo = _lesson.Get(lessionId);
            var broadcastReceiveResult = new BroadcastReceiveResult()
            {
                LessonId = sceneInfo.LessonId,
                SceneId = sceneInfo.SceneId
            };

            var channelDeviceInfos = _broadcast.Accept(lessionId);
            var teacherChannelInfo = channelDeviceInfos.Where(c => c.DeviceInfo.Type == DeviceTypes.TeacherScreen).FirstOrDefault();

            if (teacherChannelInfo != null)
            {
                broadcastReceiveResult.IsShow = true;
                broadcastReceiveResult.DataChannel = teacherChannelInfo.DataUrl;

                var monitorCheckResult = _monitor.Check(lessionId);
                if (monitorCheckResult.IsMonitor)
                {         
                    var lockDeviceIds = _monitor.GetLockDevice(monitorCheckResult.MonitorId);
                    if (lockDeviceIds.Count > 0)
                    {
                        var myDeviceId = _environment.GetMyDeviceId();
                        broadcastReceiveResult.IsShow = !lockDeviceIds.Contains(myDeviceId);
                    }
                }              
            }
           
            return broadcastReceiveResult;
        }

        /// <summary>
        /// 结束屏幕广播
        /// </summary>
        /// <param name="broadcastId">广播标识</param>
        public void StopBroadcast(string broadcastId)
        {
             _broadcast.Stop(broadcastId);
        }

        /// <summary>
        /// 更新屏幕广播
        /// </summary>
        /// <param name="lessionId">课时标识</param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public string UpdateBroadcast(string lessionId, BroadcastUpdateParam parameter)
        {
            if (parameter.IsBroadcastAll)
            {
                var deviceInfos = _environment.GetAllAccepter(lessionId);
                parameter.DeviceIds = deviceInfos.Where(d => d.Type == DeviceTypes.GroupScreen).Select(d => d.Id).ToList();
            }

            return  _broadcast.Start(lessionId, parameter.DeviceIds);
        }
    }
}