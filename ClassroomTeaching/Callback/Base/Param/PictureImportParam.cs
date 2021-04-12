using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Param
{
    /// <summary>
    /// 图片导入参数
    /// </summary>
    public class PictureImportParam
    {
        /// <summary>
        /// 文件句柄
        /// </summary>
        public String Handle { get; set; }
        /// <summary>
        /// 图片在压缩包内的文件名
        /// </summary>
        public List<String> PictureEntryNames { get; set; }
    }
}
