using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Brainstorming.WebAPI
{
    public class BrainstormingConfigSection
    {
        public const String DefaultSectionName = "Translayer.ClassroomTeaching.Brainstorming";    
        /// <summary>
        /// 学生默认头像
        /// </summary>
        public String DefaultStudentPortrait { get; set; }
    }
}