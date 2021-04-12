using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Param
{
    public class UserScanParam
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public String Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 学号
        /// </summary>
        public String Number { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public String Gender { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public String Portrait { get; set; }
    }
}