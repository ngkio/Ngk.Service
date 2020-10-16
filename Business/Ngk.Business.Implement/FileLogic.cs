using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using Thor.Framework.Common.Helper;
using Thor.Framework.Common.Helper.Cryptography;
using Thor.Framework.Data;
using Thor.Framework.Data.Model;
using Ngk.Business.Interface;
using Ngk.Common;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.Entities.Mongo;
using Ngk.DataAccess.Interface.Mongo;
using File = Ngk.DataAccess.Entities.Mongo.File;

namespace Ngk.Business.Implement
{
    public class FileLogic : IFileLogic
    {
        private readonly IFileRepository _fileRepository;
        private readonly IFileHeaderRepository _fileHeaderRepository;

        public FileLogic(IFileRepository fileRepository, IFileHeaderRepository fileHeaderRepository)
        {
            _fileRepository = fileRepository;
            _fileHeaderRepository = fileHeaderRepository;
        }

        public File GetFileById(Guid id)
        {
            return _fileRepository.GetById(id);
        }

        public FileHeader GetFileHeaderById(Guid id)
        {
            return _fileHeaderRepository.GetById(id);
        }

        public void PreSaveFile(string key, out ExcutedResult result)
        {
            var fileHeader = _fileHeaderRepository.FirstOrDefault(p => p.Key == key);
            if (fileHeader == null)
            {
                result = ExcutedResult.FailedResult(BusinessResultCode.FileNotExistYet, "");
                return;
            }
            result = ExcutedResult.SuccessResult(FileModel.FileHeaderToModel(fileHeader));
        }

        public void SaveFile(FileHeader fileHeader, byte[] content, out ExcutedResult result)
        {
            if (fileHeader.Id == default(Guid))
            {
                fileHeader.Id = Guid.NewGuid();
            }

            fileHeader.Key = GetKey(content);
            fileHeader.CreateTime = DateTimeOffset.Now;
            fileHeader.State = 1;

            PreSaveFile(fileHeader.Key, out result);
            if (result.Status == EnumStatus.Success)
            {
                return;
            }

            var file = new File { Id = fileHeader.Id, Content = content, ContentType = fileHeader.ContentType };

            try
            {
                _fileRepository.Insert(file);
                _fileHeaderRepository.Insert(fileHeader);
                result = ExcutedResult.SuccessResult(FileModel.FileHeaderToModel(fileHeader));
            }
            catch (Exception ex)
            {
                Log4NetHelper.WriteError(GetType(), ex);
                result = ExcutedResult.FailedResult(BusinessResultCode.FileSaveFail, "");
            }
        }

        private static string GetKey(byte[] content)
        {
            RIPEMD160Managed ripemd160Managed = new RIPEMD160Managed();
            var part1 = ripemd160Managed.ComputeHash(content);
            var part2 = SHA1.Create().ComputeHash(content);

            return Base58.Encode(part1.Concat(part2).ToArray());
        }


        /// <summary>
        /// 下载图片并保存在mongo中
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        public ExcutedResult DownloadImage1(string url)
        {
            ExcutedResult result = null;
            if (String.IsNullOrEmpty(url)) throw new BusinessException(BusinessResultCode.ArgumentError, "下载地址为空");
            //扩展名
            String ext = url.Substring(url.LastIndexOf('.'));
            String contentType = String.Empty;
            WebRequest request = WebRequest.Create(url);

            if (ext == ".png")
            {
                contentType = "image/png";
            }
            else if (ext == ".jpe" || ext == ".jpeg" || ext == ".jfif" || ext == ".jpg")
            {
                contentType = "image/jpeg";
            }
            if (!String.IsNullOrEmpty(contentType)) request.ContentType = contentType;

            WebResponse response = request.GetResponse();
            using (Stream reader = response.GetResponseStream())
            {
                var content = new byte[reader.Length];
                reader.Read(content, 0, (int)reader.Length);

                var fileHeader = new FileHeader
                {
                    FileExt = url.Substring(url.LastIndexOf('.')),
                    ContentType = contentType,
                    Length = reader.Length,
                    Category = 1
                };
               
                SaveFile(fileHeader, content, out result);
            }
            return result;
        }

        /// <summary>
        /// 下载图片并保存在mongo中
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        public ExcutedResult DownloadImage(string url)
        {
            ExcutedResult result = null;
            if (String.IsNullOrEmpty(url)) return result;
            //扩展名
            String ext = url.Substring(url.LastIndexOf('.'));
            String contentType = String.Empty;
            WebClient webClient = new WebClient();
            byte[] dataInfo= webClient.DownloadData(url);

            if (ext == ".png")
            {
                contentType = "image/png";
            }
            else if (ext == ".jpe" || ext == ".jpeg" || ext == ".jfif" || ext == ".jpg")
            {
                contentType = "image/jpeg";
            }

            //WebResponse response = request.GetResponse();
            //using (Stream reader = response.GetResponseStream())
            {
                
                var fileHeader = new FileHeader
                {
                    FileExt = url.Substring(url.LastIndexOf('.')),
                    ContentType = contentType,
                    Length = dataInfo.Length,
                    Category = 1
                };

                SaveFile(fileHeader, dataInfo, out result);
            }
            return result;
        }
    }
}
