using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI
{
    public class CustomConfigSection
    {
        public const String DefaultSectionName = "Translayer.ClassroomTeaching.Custom";
      
        /// <summary>
        /// 默认倒计时长（单位毫秒）
        /// </summary>
        public Int32 CountDownMillseconds { get; set; }

        /// <summary>
        /// 默认教学班名
        /// </summary>
        public String DefaultClassName { get; set; }
        /// <summary>
        /// 默认课时名
        /// </summary>
        public String DefaultLessonName { get; set; }
        /// <summary>
        /// 资料预料路由名
        /// </summary>
        public String MaterialPreviewEntranceName { get; set; }
        /// <summary>
        /// 学生默认头像
        /// </summary>
        public String DefaultStudentPortrait { get; set; }
    }
}