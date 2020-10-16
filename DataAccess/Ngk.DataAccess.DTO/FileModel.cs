using Thor.Framework.Common.IoC;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Ngk.DataAccess.Entities.Mongo;

namespace Ngk.DataAccess.DTO
{
    public class FileModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("fileExt")]
        public string FileExt { get; set; }

        [JsonProperty("category")]
        public int Category { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("fileUrl")]
        public string FileUrl { get; set; }

        public static FileModel FileHeaderToModel(FileHeader fileHeader)
        {
            IConfiguration config = AspectCoreContainer.Resolve<IConfiguration>();
            var filePreUrl = config["FilePreUrl"];
            return new FileModel
            {
                Id = fileHeader.Id.ToString(),
                FileExt = fileHeader.FileExt,
                Category = fileHeader.Category,
                Size = fileHeader.Length,
                FileUrl = $"{filePreUrl}{fileHeader.Id}?tag=client_upload"
            };
        }
    }
}
