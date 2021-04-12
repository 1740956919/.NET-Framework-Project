using LINDGE.PARA.Generic.ClassroomTeaching.Lesson.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI
{
    public class ExceptionHelper
    {
        public static string GetCodeByException(ErrorCodeException ex)
        {
            String code = "500";
            switch (ex.ErrorCode)
            {
                case 1003:
                    code = "4004";//互评已结束
                    break;
                case 10002:
                    code = "4005";//无权限加入互评
                    break;
                case ErrorCodes.RepeatCreateActiveLesson: //重复上课，上课失败
                    code = "1005";
                    break;
                case ErrorCodes.CreateActiveLessonFaild: //创建课时失败
                    code = "1006";
                    break;
                case ErrorCodes.LessonNotOpen: //课时未开始
                    code = "1010";
                    break;
                case ErrorCodes.LessonExistTeacher: //教师已存在
                    code = "1011";
                    break;
                default:
                    break;
            }
            return code;
        }
    }
}