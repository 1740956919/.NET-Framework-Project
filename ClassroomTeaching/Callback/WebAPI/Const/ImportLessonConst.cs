using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Callback.WebAPI.Const
{
    public class ImportLessonConst
    {
        /// <summary>
        /// 导入图片目录
        /// </summary>
        public const string ImportPhotoUrl = "zipstorage://Bank2.CasualStream/";
        /// <summary>
        /// 导入文件目录
        /// </summary>
        public const string ImportFileUrl = "filestorage://Bank2.Export/";
        /// <summary>
        /// 上传文件目录
        /// </summary>
        public const string UploadFileUrl = "filestorage://Bank2.CasualStream/";
        /// <summary>
        /// 文件协议
        /// </summary>
        public const string FileProtocol = "filestorage";
        /// <summary>
        /// zip协议
        /// </summary>
        public const string ZipProtocol = "zipstorage";
    }
}