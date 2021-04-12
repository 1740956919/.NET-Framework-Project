using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct
{
    public class ClassroomStateInfo
    {
        public ClassroomStateInfo()
        {
            LessonInfo = new LessonDetailInfo();
            TeacherInfo = new TeacherBasicInfo();
        }
        /// <summary>
        /// 教室状态
        /// </summary>
        public String State { get; set; }
        /// <summary>
        /// 是否关联课程
        /// </summary>
        public Boolean HasRelatedLesson { get; set; }
        /// <summary>
        /// 课程信息
        /// </summary>
        public LessonDetailInfo LessonInfo { get; set; }
        /// <summary>
        /// 是否关联教师
        /// </summary>
        public Boolean HasRelatedTeacher { get; set; }
        /// <summary>
        /// 教师信息
        /// </summary>
        public TeacherBasicInfo TeacherInfo { get; set; }
        /// <summary>
        /// 工作行为标识
        /// </summary>
        public String WorkBehaviorId { get; set; }
    }
}