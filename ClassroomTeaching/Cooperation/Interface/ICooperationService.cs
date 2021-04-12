using LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI.Interface
{
    /// <summary>
    /// 协作讨论服务
    /// </summary>
    public interface ICooperationService
    {
        /// <summary>
        /// 获取协作讨论状态
        /// </summary>
        /// <param name="cooperationDiscussId"></param>
        /// <returns></returns>
        CooperationStatus GetCooperationStatus(String cooperationDiscussId);
        /// <summary>
        /// 获取组成员信息
        /// </summary>
        /// <param name="cooperationDiscussId"></param>
        /// <returns></returns>
        DiscussionMemberInfo GetGroupMembers(String cooperationDiscussId);
        /// <summary>
        /// 更换主持人
        /// </summary>
        /// <param name="cooperationDiscussId"></param>
        /// <param name="memberId"></param>
        void ChangeChair(String cooperationDiscussId, String memberId);
        /// <summary>
        /// 主持讨论
        /// </summary>
        /// <param name="cooperationDiscussId"></param>
        /// <param name="parameter"></param>
        void ChairDiscussion(String cooperationDiscussId, DiscussionChairParam parameter);
        /// <summary>
        /// 获取历史消息信息
        /// </summary>
        /// <param name="cooperationDiscussId"></param>
        /// <returns></returns>
        HistoryMessageInfo GetHistoryMessageInfo(String cooperationDiscussId);
        /// <summary>
        /// 查询指定时间后的消息
        /// </summary>
        /// <param name="cooperationDiscussId"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        List<MessageInfo> QueryPeriodMessages(String cooperationDiscussId, MessageQueryParam parameter);
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="cooperationDiscussId"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        MessageInfo SendMessage(String cooperationDiscussId, MessageSendParam parameter);
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="cooperationDiscussId"></param>
        /// <param name="MessageId"></param>
        /// <returns></returns>
        FileDownloadResult DownloadFile(String cooperationDiscussId, String MessageId);
    }
}
