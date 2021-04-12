using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Authorization.WebAPI.Const
{
    /// <summary>
    /// 终端类型
    /// </summary>
    public class DeviceType
    {
        /// <summary>
        /// 教师屏
        /// </summary>
        public const string Teacher = "TEACHER";
        /// <summary>
        /// 小组屏
        /// </summary>
        public const string Group = "GROUP";

        public static string ConvertToSceneDeviceType(string deviceType)
        {
            var result = string.Empty;
            switch (deviceType)
            {
                case DeviceType.Group:
                    result = Generic.ClassroomTeaching.Scene.Const.DeviceTypes.GroupScreen; break;
                case DeviceType.Teacher:
                    result = Generic.ClassroomTeaching.Scene.Const.DeviceTypes.TeacherScreen; break;
            }
            return result;
        }

        public static string ConvertToAuthorizationDeviceType(string deviceType)
        {
            var result = string.Empty;
            switch (deviceType)
            {
                case Generic.ClassroomTeaching.Scene.Const.DeviceTypes.GroupScreen:
                    result = DeviceType.Group; break;
                case Generic.ClassroomTeaching.Scene.Const.DeviceTypes.TeacherScreen:
                    result = DeviceType.Teacher; break;
            }
            return result;
        }
    }
}