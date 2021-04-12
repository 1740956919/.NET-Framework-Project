using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Discussion.WebAPI
{
    public class DiscussionConfigSection
    {
        public const String DefaultSectionName = "Translayer.ClassroomTeaching.Discussion";

        /// <summary>
        /// 学生默认头像
        /// </summary>
        public String DefaultStudentPortrait { get; set; }
    }
}