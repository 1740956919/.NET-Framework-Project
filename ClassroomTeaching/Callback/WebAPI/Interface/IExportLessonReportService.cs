using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Callback.WebAPI.Interface
{
    public interface IExportLessonReportService
    {
        /// <summary>
        /// 导出课堂报告活动图片
        /// </summary>
        /// <param name="parametor"></param>
        void ExportActivityPictures(PictureExportParam parametor);
    }
}