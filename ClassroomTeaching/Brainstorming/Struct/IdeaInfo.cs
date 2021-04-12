using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI.Struct
{
    public class IdeaInfo
    {
        /// <summary>
        /// 标识
        /// </summary>
        public String IdeaId { get; set; }
        /// <summary>
        /// 点子内容
        /// </summary>
        public String IdeaContent { get; set; }
        /// <summary>
        /// 作者标识
        /// </summary>
        public String AuthorId { get; set; }
    }
}