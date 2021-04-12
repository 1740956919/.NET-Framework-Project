using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Result;
using LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Quiz.WebAPI.Interface
{
    public interface IInteractionStateService
    {
        /// <summary>
        /// 查询测验的状态
        /// </summary>
        /// <param name="classroomId"></param>
        /// <returns></returns>
        QuizState GetQuizState(string classroomId);
    }
}