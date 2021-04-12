using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI
{
    /// <summary>
    /// 互动教学配置
    /// </summary>
    public class InteractionConfigSection
    {
        public const String DefaultSectionName = "Translayer.ClassroomTeaching.Interaction";

        /// <summary>
        /// 学生默认头像
        /// </summary>
        public String DefaultStudentPortrait { get; set; }
        /// <summary>
        /// 资料预料路由名
        /// </summary>
        public String MaterialPreviewEntranceName { get; set; }

        /// <summary>
        /// 教学班默认封面
        /// </summary>
        public String DefaultClassCover { get; set; }

        /// <summary>
        /// 默认教学班名
        /// </summary>
        public String DefaultClassName { get; set; }
        /// <summary>
        /// 默认课时名
        /// </summary>
        public String DefaultLessonName { get; set; }

        /// <summary>
        /// 加密方式
        /// </summary>
        public String Encryption { get; set; }
        /// <summary>
        /// 工作模式
        /// </summary>
        public String WorkMode { get; set; }
        public String WifiSSID { get; set; }
        public String WifiPassword { get; set; }

    }
}