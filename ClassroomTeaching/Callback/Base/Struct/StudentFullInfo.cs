using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Const;
using System;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Struct
{
    public class StudentFullInfo
    {
        /// <summary>
        /// 用户相关码 | 必填
        /// </summary>
        public string CorrelativeCode { get; set; }
        /// <summary>
        /// 用户名称 | 可选
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 用户工号 | 可选
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 性别 | 可选
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// 头像 | 可选
        /// </summary>
        public string Portrait { get; set; }
        /// <summary>
        /// 成员标识
        /// </summary>
        public string MemberId { get; set; }
        /// <summary>
        /// 是否登录
        /// </summary>
        public String IsLogon { get; set; }
        /// <summary>
        /// 是否签到
        /// </summary>
        public String IsSigned { get; set; }
        /// <summary>
        /// 最近活跃时间
        /// </summary>
        public string LatestActiveTime { get; set; }
    }
}