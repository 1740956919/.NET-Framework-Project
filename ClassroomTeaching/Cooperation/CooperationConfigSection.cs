using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Cooperation.WebAPI
{
    public class CooperationConfigSection
    {

        public const String DefaultSectionName = "Translayer.ClassroomTeaching.Cooperation";

        /// <summary>
        /// 学生默认头像
        /// </summary>
        public String DefaultStudentPortrait { get; set; }
    }
}