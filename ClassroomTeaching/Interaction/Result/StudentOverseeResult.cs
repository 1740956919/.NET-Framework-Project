using LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Interaction.WebAPI.Result
{
    public class StudentOverseeResult
    {
        public StudentOverseeResult()
        {
            LessonMoment = new LessonMoment();
        }
        public Boolean IsOnClass { get; set; }
        public LessonMoment LessonMoment { get; set; }
    }
}