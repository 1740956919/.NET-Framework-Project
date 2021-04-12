using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Param
{
    public class ClassStartParam
    {
        /// <summary>
        /// 是否关联云端
        /// </summary>
        public Boolean IsRelateCloud { get; set; }
        /// <summary>
        /// 云端课时信息
        /// </summary>
        public CloudLessonInfo CloudLessonInfo { get; set; }
    }
}