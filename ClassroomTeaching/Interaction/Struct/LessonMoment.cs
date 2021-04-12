using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    public class LessonMoment
    {
        public LessonMoment()
        {
            SignInfo = new SignState();
        }

        /// <summary>
        /// 教学班名
        /// </summary>
        public String TeachingClassName { get; set; }
        /// <summary>
        /// 课时标识
        /// </summary>
        public String lessonId { get; set; }
        /// <summary>
        /// 名单标识
        /// </summary>
        public String NameListId { get; set; }
        /// <summary>
        /// 签到信息
        /// </summary>
        public SignState SignInfo { get; set; }
    }
}