using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.Param;
using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.WebAPI.Const;
using LINDGE.PARA.Translayer.ClassroomTeaching.Callback.WebAPI.Interface;
using LINDGE.PARA.Translayer.Static.Picture.WebAPI;
using LINDGE.PARA.Translayer.Static.Picture.WebAPI.Param;
using LINDGE.Proxy;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;

namespace LINDGE.PARA.Translayer.ClassroomTeaching.Callback.WebAPI.Service
{
    public class ExportLessonReportService : IExportLessonReportService
    {
        private readonly IIOProvider _ioProvider = null;
        private readonly IPictureSet _pictureSet = null;

        public ExportLessonReportService(IIOProvider ioProviderService,
             IProxy<IPictureSet> pictureSetProxy)
        {
            _ioProvider = ioProviderService;
            _pictureSet = pictureSetProxy.GetObject();
        }

        /// <summary>
        /// 生成图片文件
        /// </summary>
        /// <param name="parametor"></param>
        public void ExportActivityPictures(PictureExportParam parametor)
        {
            var pictureIds = parametor.PictureIds;
            var zipFilePath = parametor.LessonReportFilePath.Replace(SchemaNames.ZipStorage, SchemaNames.FileStorage);

            //导出图片
            var pictureExportResult = _pictureSet.Export(new PictureSetExportParam() { 
                PictureIds = pictureIds,
                IsExportAllSuffix = true
            });
            if(pictureExportResult.Count > 0)
            {
                using (var zipFileStream = _ioProvider.Open(zipFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                using (var zipArchive = new ZipArchive(zipFileStream, ZipArchiveMode.Update))
                {
                    foreach (var pictureResult in pictureExportResult)
                    {
                        if (_ioProvider.Exists(pictureResult.Location))
                        {
                            using (var pictureFileStream = _ioProvider.Open(pictureResult.Location, FileMode.Open, FileAccess.Read))
                            {
                                var picturePath = Path.Combine(Path.GetDirectoryName(pictureResult.PictureId), Path.GetFileName(pictureResult.PictureId));
                                var pictureId = picturePath.Replace(Path.DirectorySeparatorChar, '-');
                                var fileFullName = Path.Combine(ReportFIleNames.RecordPictures, pictureId);
                                var entry = zipArchive.CreateEntry(fileFullName);
                                var entryStream = entry.Open();
                                pictureFileStream.CopyTo(entryStream);
                            }
                            _ioProvider.Delete(pictureResult.Location);
                        }
                    }
                }
            }       
        }
       
    }
}