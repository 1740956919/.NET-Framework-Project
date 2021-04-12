using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Struct
{
    /// <summary>
    /// 分组结果
    /// </summary>
    public class GroupResult
    {
        public GroupResult()
        {
            Groups = new List<GroupFullInfo>();
            UngroupStudents = new List<String>();
        }
        /// <summary>
        /// 是否正在分组
        /// </summary>
        public Boolean IsGrouping { get; set; }
        /// <summary>
        /// 分组持续时间
        /// </summary>
        public Int32 DuringSeconds { get; set; }
        /// <summary>
        /// 小组列表
        /// </summary>
        public List<GroupFullInfo> Groups { get; set; }
        /// <summary>
        /// 已分组的人数
        /// </summary>
        public Int32 GroupedStudentCount { get; set; }
        /// <summary>
        /// 待加入分组的总人数
        /// </summary>
        public Int32 TotalStudentCount { get; set; }
        /// <summary>
        /// 未分组的学生姓名列表
        /// </summary>
        public List<String> UngroupStudents { get; set; }
        /// <summary>
        /// 是否正在上课
        /// </summary>
        public Boolean IsProcessing { get; set; }
    }
}