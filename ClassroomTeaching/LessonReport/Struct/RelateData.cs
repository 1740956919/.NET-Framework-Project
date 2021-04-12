using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.LessonReport.WebAPI.Struct
{
    /// <summary>
    /// 课堂记录关联数据
    /// </summary>
    public class RelateData
    {
        public Boolean IsRelated { get; set; }

        public String RelateId { get; set; }

        public String Title { get; set; }

        public String RelateType { get; set; }
    }
}