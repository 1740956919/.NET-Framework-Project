using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    /// <summary>
    /// 学生互评打分信息
    /// </summary>
    public class MemberScoreInfo
    {
        /// <summary>
        /// 成员标识
        /// </summary>
        public string MemberId { get; set; }
        /// <summary>
        /// 是否递交分数
        /// </summary>
        public bool IsSubmited { get; set; }
        /// <summary>
        /// 打分
        /// </summary>
        public float Score { get; set; }
    }
}