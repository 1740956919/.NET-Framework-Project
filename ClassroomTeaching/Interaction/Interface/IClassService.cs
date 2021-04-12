using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct;
using LINDGE.PARA.Translayer.ClassroomTeaching.Interation.WebAPI.Result;
using System;
using System.Collections.Generic;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Interface
{
    /// <summary>
    /// 班级管理接口
    /// </summary>
    public interface IClassService
    {
        /// <summary>
        /// 获取教室二维码
        /// </summary>
        /// <param name="classroomId"></param>
        /// <returns></returns>
        QRCodeInfo GetQRCodeInfo(String classroomId);
        /// <summary>
        /// 总览学生
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        StudentOverseeResult OverseeStudent(String classroomId);
        /// <summary>
        /// 查询学生列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<StudentInfo> QueryStudents(String nameListId);
        /// <summary>
        /// 开始签到
        /// </summary>
        /// <param name="classroomId"></param>
        /// <returns></returns>
        SignBeginResult BeginSign(String classroomId);
        /// <summary>
        /// 获取教室状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ClassroomStateInfo GetClassroomState(String classroomId);
        /// <summary>
        /// 上课
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        ClassStartResult BeginClass(String classroomId, ClassStartParam parameter);
        /// <summary>
        /// 停止签到
        /// </summary>
        /// <param name="signSessonId"></param>
        void StopSign(String signSessonId);
        /// <summary>
        /// 学生注册并登录
        /// </summary>
        /// <param name="parameter"></param>
        void LoginStudentDevice(String classroomId, String userDeviceCode);
        /// <summary>
        /// 下课
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void EndClass(String classroomId);
        /// <summary>
        /// 加入教室
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        StudentAttendClassResult AttendClass(String classroomId, StudentAttendClassParam parameter);
        /// <summary>
        /// 开始课程导入
        /// </summary>
        /// <param name="lessonId"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        ImportStartResult StartImport(String lessonId, ImportStartParam parameter);
        /// <summary>
        /// 执行数据导入
        /// </summary>
        /// <param name="id"></param>
        /// <param name="handle"></param>
        void ExecuteImport(String wordBehaviorId, String handle);
        /// <summary>
        /// 停止课程导入
        /// </summary>
        /// <param name="workBehaviorId"></param>
        void StopImport(String workBehaviorId);
    }
}
