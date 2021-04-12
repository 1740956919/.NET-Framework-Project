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
    /// 提供有关屏幕广播的能力
    /// </summary>
    public interface IBroadcastService
    {
        /// <summary>
        /// 查询屏幕广播状态
        /// </summary>
        /// <param name="sceneId">场景标识</param>
        /// <returns></returns>
        WorkStateInfo GetBroadcastWorkState(string sceneId);
        /// <summary>
        /// 更新屏幕广播
        /// </summary>
        /// <param name="sceneId">场景标识</param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        string UpdateBroadcast(string sceneId, BroadcastUpdateParam parameter);
        /// <summary>
        /// 结束屏幕广播
        /// </summary>
        /// <param name="broadcastId">广播标识</param>
        void StopBroadcast(string broadcastId);
        /// <summary>
        /// 小组屏接收广播
        /// </summary>
        /// <param name="sceneId">场景标识</param>
        /// <returns></returns>
        BroadcastReceiveResult ReceiveBroadcast(string sceneId);
    }
}