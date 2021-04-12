using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Const
{
    /// <summary>
    /// 群的角色名
    /// </summary>
    public class GroupRoleNames
    {
        /// <summary>
        /// 教学角色组名
        /// </summary>
        public const String Teaching = "TEACHING";
        /// <summary>
        /// 讨论角色组名
        /// </summary>
        public const String Discuss = "DISCUSS";
        #region 终端设备
        /// <summary>
        /// 教师工具
        /// </summary>
        public const string TeachTool = "TEACHTOOL";
        /// <summary>
        /// 讨论工具
        /// </summary>
        public const string DisscussTool = "DISSCUSSTOOL";
        #endregion

        #region 教学角色
        /// <summary>
        /// 教师
        /// </summary>
        public const string Teacher = "TEACHER";
        /// <summary>
        /// 学生
        /// </summary>
        public const string Student = "STUDENT";
        #endregion

        #region 讨论小组角色
        /// <summary>
        /// 主持人
        /// </summary>
        public const string Chair = "CHAIR";
        /// <summary>
        /// 参与成员
        /// </summary>
        public const string Participant = "PARTICIPANT";
        #endregion

        public static string ConvertRoleNameFromRank(int rank)
        {
            var role = string.Empty;
            switch (rank)
            {
                case GroupRanks.Chair:
                    role = GroupRoleNames.Chair; break;
                case GroupRanks.DisscussTool:
                    role = GroupRoleNames.DisscussTool; break;
                case GroupRanks.Participant:
                    role = GroupRoleNames.Participant; break;
                case GroupRanks.Student:
                    role = GroupRoleNames.Student; break;
                case GroupRanks.Teacher:
                    role = GroupRoleNames.Teacher; break;
                case GroupRanks.TeachTool:
                    role = GroupRoleNames.TeachTool; break;
                default:
                    throw new Exception("不支持角色");
            }
            return role;
        }
    }
}