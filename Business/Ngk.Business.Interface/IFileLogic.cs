using System;
using Thor.Framework.Data.Model;
using Ngk.DataAccess.Entities.Mongo;

namespace Ngk.Business.Interface
{
    public interface IFileLogic
    {
        void SaveFile(FileHeader fileHeader, byte[] content, out ExcutedResult result);
        File GetFileById(Guid id);
        FileHeader GetFileHeaderById(Guid id);
        void PreSaveFile(string key, out ExcutedResult result);

        /// <summary>
        /// 下载图片并保存至mongo
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        ExcutedResult DownloadImage(string url);
    }
}
