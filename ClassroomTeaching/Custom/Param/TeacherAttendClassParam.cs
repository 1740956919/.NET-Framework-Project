using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Param
{
    public class TeacherAttendClassParam
    {
        public TeacherAttendClassParam()
        {
            this.LessonInfo = new ExportLessonInfo();
            this.TeacherInfo = new TeacherInfo();
        }

        /// <summary>
        /// 用户设备唯一码
        /// </summary>
        public string UserDeviceCode { get; set; }
        /// <summary>
        /// 场景标识
        /// </summary>

        public string SceneId { get; set; }
        /// <summary>
        /// 登录单位标识
        /// </summary>

        public string LogonUnitId { get; set; }
        /// <summary>
        /// 是否导入课时
        /// </summary>

        public bool IsLoadLesson { get; set; }
        /// <summary>
        /// 导入的课时信息
        /// </summary>

        public ExportLessonInfo LessonInfo { get;set; }
        /// <summary>
        /// 教师用户信息
        /// </summary>

        public TeacherInfo TeacherInfo { get; set; }
    }
}