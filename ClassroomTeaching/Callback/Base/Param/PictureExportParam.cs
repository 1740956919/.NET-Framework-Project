using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Param
{
    public class PictureExportParam
    {
        public PictureExportParam()
        {
            PictureIds = new List<String>();
        }

        /// <summary>
        /// 图片句柄
        /// </summary>
        public List<String> PictureIds { get; set; }
        /// <summary>
        /// 课堂记录文件路径
        /// </summary>
        public String LessonReportFilePath { get; set; }
    }
}
