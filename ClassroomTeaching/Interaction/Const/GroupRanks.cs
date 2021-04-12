using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Const
{
    public class GroupRanks
    {
        #region
        /// <summary>
        /// 教学工具
        /// </summary>
        public const int TeachTool = 1000;
        /// <summary>
        /// 讨论工具
        /// </summary>
        public const int DisscussTool = 2000;
        #endregion

        #region
        /// <summary>
        /// 教师
        /// </summary>
        public const int Teacher = 1001;
        /// <summary>
        /// 学生
        /// </summary>
        public const int Student = 1002;
        #endregion

        #region
        /// <summary>
        /// 主持人
        /// </summary>
        public const int Chair = 2001;
        /// <summary>
        /// 参与成员
        /// </summary>
        public const int Participant = 2002;
        #endregion

        public static int ConvertRankFromRoleName(string roleName)
        {
            var rank = 0;
            switch (roleName)
            {
                case GroupRoleNames.Chair:
                    rank = GroupRanks.Chair; break;
                case GroupRoleNames.DisscussTool:
                    rank = GroupRanks.DisscussTool; break;
                case GroupRoleNames.Participant:
                    rank = GroupRanks.Participant; break;
                case GroupRoleNames.Student:
                    rank = GroupRanks.Student; break;
                case GroupRoleNames.Teacher:
                    rank = GroupRanks.Teacher; break;
                case GroupRoleNames.TeachTool:
                    rank = GroupRanks.TeachTool; break;
                default:
                    throw new Exception("不支持角色");
            }
            return rank;
        }
    }
}