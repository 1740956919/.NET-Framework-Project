using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Param
{
    /// <summary>
    /// 课程资料添加参数
    /// </summary>
    public class LessonMaterialAddParam
    {
        /// <summary>
        /// 文件夹名
        /// </summary>
        public String FolderName { get; set; }
        /// <summary>
        /// 文件句柄
        /// </summary>
        public String Handle { get; set; }
        /// <summary>
        /// 课时标识
        /// </summary>
        public String LessonId { get; set; }
        /// <summary>
        /// 库标识
        /// </summary>
        public String LibraryId { get; set; }
        /// <summary>
        /// 库目录项标识
        /// </summary>
        public String CatalogItemId { get; set; }
    }
}
