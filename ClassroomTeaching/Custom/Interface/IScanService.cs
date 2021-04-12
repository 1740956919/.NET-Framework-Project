using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Custom.WebAPI.Interface
{
    public interface IScanService
    {
        /// <summary>
        /// 获取所有可以下载的课时列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        LessonQueryResult QueryAllDownLoadLessons(LessonQueryParam parameter);
        /// <summary>
        /// 下载课时并上课
        /// </summary>
        /// <param name="parameter"></param>
        void DownLoadLessonAndAttendClass(TeacherAttendClassParam parameter);
        /// <summary>
        /// 学生注册并登录
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        StudentLoginResult LoginStudentDevice(StudentLoginParam parameter);
        /// <summary>
        /// 加入教室上课
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        StudentAttendClassResult AttendClass(StudentAttendClassParam parameter);
    }
}