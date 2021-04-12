using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI.Struct
{
    public class OptionalGroupInfo
    {
        public OptionalGroupInfo()
        {
            Groups = new List<GroupBasicInfo>();
        }

        /// <summary>
        /// 小组信息列表
        /// </summary>
        public List<GroupBasicInfo> Groups { get; set; }
        /// <summary>
        /// 是否选择分组
        /// </summary>
        public Boolean IsSelectedGroup { get; set; }
        /// <summary>
        /// 选择的小组标识
        /// </summary>
        public String SelectedGroupId { get; set; }
    }
}