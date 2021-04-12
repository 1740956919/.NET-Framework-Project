using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Param
{
    public class CategoryCreateParam
    {
        public CategoryCreateParam()
        {
            this.IdeaIds = new List<String>();
        }

        /// <summary>
        /// 分类名称
        /// </summary>
        public String CategoryName { get; set; }
        /// <summary>
        /// 点子标识列表
        /// </summary>
        public List<String> IdeaIds { get; set; }
    }
}