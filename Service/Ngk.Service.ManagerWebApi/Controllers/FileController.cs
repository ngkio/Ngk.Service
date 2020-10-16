using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ngk.Business.Interface;
using Ngk.DataAccess.Entities.Mongo;

namespace Ngk.Service.ManagerWebApi.Controllers
{       
    public class FileController : Controller
    {
        private readonly IFileLogic _logic;
        public FileController(IFileLogic fileLogic)
        {
            _logic = fileLogic;
        }

        [HttpPost]
        public IActionResult Upload(IFormFile file, int fileCategory)
        {
            var fileHeader = new FileHeader
            {
                FileExt = file.FileName.Substring(file.FileName.LastIndexOf('.')),
                ContentType = file.ContentType,
                Length = file.Length,
                Category = fileCategory
            };

            var stream = file.OpenReadStream();
            var content = new byte[stream.Length];
            stream.Read(content, 0, (int)stream.Length);

            _logic.SaveFile(fileHeader, content, out var result);
            return Json(result);
        }

        /// <summary>
        /// LOGO上传,无需登录，但需限制文件大小与类型
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileCategory"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost,HttpOptions]
        public IActionResult UploadLogo(IFormFile file, int fileCategory)
        {
            var fileHeader = new FileHeader
            {
                FileExt = file.FileName.Substring(file.FileName.LastIndexOf('.')),
                ContentType = file.ContentType,
                Length = file.Length,
                Category = fileCategory
            };
            var stream = file.OpenReadStream();
            var content = new byte[stream.Length];
            stream.Read(content, 0, (int)stream.Length);

            _logic.SaveFile(fileHeader, content, out var result);
            return Json(result);
        }

        [HttpPost]
        public IActionResult PreUpload(string fileKey)
        {
            _logic.PreSaveFile(fileKey, out var result);
            return Json(result);
        }

        [HttpGet]
        [AllowAnonymous]
        [ResponseCache(Duration = 31536000)]
        public IActionResult Get(string id)
        {
            if (!Guid.TryParse(id, out var gid))
            {
                return NotFound();
            }

            var file = _logic.GetFileById(gid);
            if (file == null)
            {
                return NotFound();
            }

            return File(file.Content, file.ContentType);
        }
    }

}
