using LINDGE.PARA.Generic.ClassroomTeaching.Scene.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Screen.WebAPI.Logic
{
    public class ConvertDeviceType
    {
        /// <summary>
        /// 将设备字符串转换成枚举
        /// </summary>
        /// <param name="value">设备类型</param>
        /// <returns></returns>
        public static DeviceCategory ConvertEnumFromString(String value)
        {
            var deviceCategory = DeviceCategory.Unknown;
            switch (value)
            {
               
                case "ANDROID":
                    deviceCategory = DeviceCategory.Android; 
                    break;
                case "IOS": 
                    deviceCategory = DeviceCategory.IOS; 
                    break;
                default:
                    deviceCategory = DeviceCategory.Unknown;
                    break;
                
            }
            return deviceCategory;
        }
    }
}