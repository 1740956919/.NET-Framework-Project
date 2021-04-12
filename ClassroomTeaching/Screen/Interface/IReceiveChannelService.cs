using LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Interface
{
    /// <summary>
    /// 提供接收画面的能力
    /// </summary>
    public interface IReceiveChannelService
    {
        /// <summary>
        /// 开始接收画面
        /// </summary>
        /// <param name="sceneId">场景标识</param>
        /// <param name="screenType">画面类型</param>
        /// <returns>接收标识</returns>
        string StartReceiveChannel(string sceneId, string screenType);
        /// <summary>
        /// 获取画面
        /// </summary>
        /// <param name="paramter"></param>
        /// <returns></returns>
        List<ChannelInfo> ReceiveChannel(ReceiveChannelParam parameter);
        /// <summary>
        /// 停止接收画面
        /// </summary>
        /// <param name="receiveId">接收标识</param>
        /// <param name="screenType">画面类型</param>
        void StopReceiveChannel(string receiveId, string screenType);
        /// <summary>
        /// 检查画面是否包含数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        List<ChannelInfo> ProofChannelData(ChannelProofParam parameter);
        /// <summary>
        /// 检查通道是否活跃
        /// </summary>
        /// <param name="channelId">通道标识</param>
        /// <returns></returns>
        ChannelAliveResult CheckChannelAlive(string channelId);
        /// <summary>
        /// 开始接收手机投屏
        /// </summary>
        /// <param name="sceneId">场景标识</param>
        /// <returns>投屏标识</returns>
        string StartReceiveProjection(string sceneId);
        /// <summary>
        /// 获取手机投屏
        /// </summary>
        /// <param name="sceneId">课时标识</param>
        /// <returns></returns>
        ProjectionResult GetProjection(string sceneId);
        /// <summary>
        /// 停止接收手机投屏
        /// </summary>
        /// <param name="projectionId">投屏标识</param>
        void StopReceiveProjection(string projectionId);
        /// <summary>
        /// 锁定屏幕
        /// </summary>
        /// <param name="monitorId">监控标识</param>
        /// <param name="parameter"></param>
        void LockDevice(string monitorId, LockDeviceParam parameter);
        /// <summary>
        /// 获取所有的投屏通道
        /// </summary>
        /// <param name="lessonId">课时标识</param>
        /// <param name="deviceType">设备类型</param>
        /// <returns></returns>
        List<ScreenChannelInfo> GetAllChannels(string lessonId, string deviceType);
    }
}