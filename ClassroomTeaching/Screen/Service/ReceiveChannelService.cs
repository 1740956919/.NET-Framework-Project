using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Const;
using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Service.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Interface;
using LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Logic;
using LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Service
{
    public class ReceiveChannelService : IReceiveChannelService
    {
        private readonly IMonitor _monitor = null;
        private readonly IProjection _projection = null;
        private readonly IEnvironment _environment = null;

        public ReceiveChannelService(
            IMonitor monitorService,        
            IProjection projectionService,
            IEnvironment environmentService
            )
        {
            _monitor = monitorService;
            _projection = projectionService;
            _environment = environmentService;
        }
        
        /// <summary>
        /// 检查通道是否活跃
        /// </summary>
        /// <param name="channelId">通道标识</param>
        /// <returns></returns>
        public ChannelAliveResult CheckChannelAlive(string channelId)
        {
            var result = new ChannelAliveResult();
            var id = Convert.ToInt32(channelId);
            result.IsAlive =  _projection.VerifyChannelIsWorking( id );
            return result;
        }

        /// <summary>
        /// 获取手机投屏
        /// </summary>
        /// <param name="lessonId">课时标识</param>
        /// <returns></returns>
        public ProjectionResult GetProjection(string lessonId)
        {
            var projectionResult = new ProjectionResult();
            var projectionCheckResult = _projection.Check(lessonId);
            if (projectionCheckResult.IsProject)
            {
                //手机投屏通道信息
                var channelInfos = QueryProjection(lessonId, projectionCheckResult.ProjectionId);
                projectionResult.IsProjection = true;
                projectionResult.ProjectionId = projectionCheckResult.ProjectionId;
                projectionResult.ChannelInfos = channelInfos;
            }
            return projectionResult;
        }

        /// <summary>
        /// 锁定一组设备
        /// </summary>
        /// <param name="monitorId">监控标识</param>
        /// <param name="parameter"></param>
        public void LockDevice(string monitorId, LockDeviceParam parameter)
        {
            _monitor.Lock(monitorId, parameter.DeviceIds, true);
        }

        /// <summary>
        /// 检查画面是否包含数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public List<ChannelInfo> ProofChannelData(ChannelProofParam parameter)
        {
            var channelIds = parameter.ChannelIds.Select(c => Convert.ToInt32(c)).ToList();
            var channelProofResult = _projection.GetChannel(parameter.LessonId, channelIds);
            return channelProofResult.Select(c => new ChannelInfo() { 
                Id = c.ChannelId.ToString(),
                HasReceiveData = c.IsProjection,
                DataChannel = c.DataUrl,
                DeviceName = c.ChannelName,
                TotalFrameCount = c.TotalFrameCount
            }).ToList();
        }

        /// <summary>
        /// 获取画面
        /// </summary>
        /// <param name="receiveId">接收标识</param>
        /// <param name="screenType">画面类型</param>
        /// <returns></returns>
        public List<ChannelInfo> ReceiveChannel(ReceiveChannelParam parameter)
        {
            var screenType = parameter.ScreenType;
            var receiveId = parameter.ReceiveId;
            var lessonId = parameter.LessonId;

            var channelInfos = new List<ChannelInfo>();
            if (screenType == ScreenType.phoneScreen)
            {
                //手机投屏通道信息
                channelInfos = QueryProjection(lessonId, receiveId);
            }
            else if(screenType == ScreenType.groupScreen)
            {
                //监控的设备
                var monitorDevices = _monitor.Get(receiveId);
                channelInfos = monitorDevices.Select(d => new ChannelInfo()
                {
                    Id = d.DeviceInfo.Id,
                    DeviceName = d.DeviceInfo.Name,
                    DataChannel = d.DataUrl,
                    HasReceiveData = true
                }).ToList();
            }

            return channelInfos;
        }

        /// <summary>
        /// 开始接收画面
        /// </summary>
        /// <param name="lessonId">场景标识</param>
        /// <param name="screenType">画面类型</param>
        /// <returns>接收标识</returns>
        public string StartReceiveChannel(string lessonId, string screenType)
        {
            var receiveId = String.Empty;
            if(screenType == ScreenType.groupScreen)
            {
                var projectionCheckResult = _projection.Check(lessonId);
                if (projectionCheckResult.IsProject)
                {
                    _projection.Stop(projectionCheckResult.ProjectionId);
                }
              
                //所有小组屏设备标识
                var deviceInfos = _environment.GetAllAccepter(lessonId);
                var allGroupDeviceIds = deviceInfos.Where(d => d.Type == DeviceTypes.GroupScreen).Select(d => d.Id).ToList();
                //开始监控屏幕
                receiveId = _monitor.Start(lessonId, allGroupDeviceIds);              
            }
            else
            {
                var monitorCheckResult = _monitor.Check(lessonId);
                if (monitorCheckResult.IsMonitor)
                {
                    _monitor.Stop(monitorCheckResult.MonitorId);
                }
               
                //开始接收手机投屏
                receiveId = _projection.Start(lessonId);            
            }
            return receiveId;
        }

        /// <summary>
        /// 开始接收手机投屏
        /// </summary>
        /// <param name="sceneId">场景标识</param>
        /// <returns>投屏标识</returns>
        public string StartReceiveProjection(string sceneId)
        {
            return _projection.Start(sceneId);
        }

        /// <summary>
        /// 停止接收画面
        /// </summary>
        /// <param name="receiveId">接收标识</param>
        /// <param name="screenType">画面类型</param>
        public void StopReceiveChannel(string receiveId, string screenType)
        {
            if (screenType == ScreenType.groupScreen)
            {
                _monitor.Stop(receiveId);
            }
            else if (screenType == ScreenType.phoneScreen)
            {
                _projection.Stop(receiveId);
            }
        }

        /// <summary>
        /// 停止接收手机投屏
        /// </summary>
        /// <param name="lessonId">课时标识</param>
        /// <param name="projectionId">投屏标识</param>
        public void StopReceiveProjection(string projectionId)
        {
            _projection.Stop(projectionId);
        }

        /// <summary>
        /// 获取手机投屏的通道信息
        /// </summary>
        /// <param name="lessonId">课时标识</param>
        /// <param name="projectionId">投屏标识</param>
        /// <returns></returns>
        private List<ChannelInfo> QueryProjection(string lessonId, string projectionId)
        {
            var projectiontInfos = _projection.Accept(lessonId, projectionId);
            var channelIds = projectiontInfos.Select(p => Convert.ToInt32(p.ChannelId)).ToList();
            var channelProofResult = _projection.GetChannel(lessonId, channelIds);
            var channelProofMap = channelProofResult.Where(c => c.IsProjection).ToDictionary(c => c.ChannelId, c => c.ChannelName);         

            return projectiontInfos.Select(p => new ChannelInfo() {
                Id = p.ChannelId.ToString(),
                HasReceiveData = channelProofMap.ContainsKey(p.ChannelId),
                DeviceName = channelProofMap.ContainsKey(p.ChannelId) ? channelProofMap[p.ChannelId] : p.ChannelName,
                DataChannel = p.DataUrl
            }).ToList();
        }

        /// <summary>
        /// 获取所有的投屏通道
        /// </summary>
        /// <param name="lessonId">课时标识</param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        public List<ScreenChannelInfo> GetAllChannels(string lessonId, string deviceType)
        {
            var deviceCategory = ConvertDeviceType.ConvertEnumFromString(deviceType);
            var channelInfos = _projection.GetAvailableChannels(lessonId, deviceCategory);

            return channelInfos.Where(c => c.ConnectUrl != null).Select(c => new ScreenChannelInfo()
            {
                Name = c.ChannelName,
                IP = Regex.Match(c.ConnectUrl, @"\d+\.\d+\.\d+\.\d*").Value,
                Port = Regex.Match(c.ConnectUrl, @"\:\d+").Value.Substring(1),
            }).ToList();
        }
    }
}