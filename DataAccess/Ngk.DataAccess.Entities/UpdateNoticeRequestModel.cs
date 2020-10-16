using Thor.Framework.Data.Model;
using Ngk.Common;
using Ngk.DataAccess.Entities;

namespace Ngk.DataAccess.Entities
{
    public class UpdateNoticeRequestModel : NoticeRequestModel
    {
        /// <summary>
        /// Id,
        /// </summary>
        public string Id { get; set; }


        /// <summary>
        /// 修改
        /// </summary>
        public void UpdateVerify()
        {
            if (string.IsNullOrEmpty(Id)) throw new BusinessException(BusinessResultCode.ArgumentError, "参数错误或无效");
        }
    }
}
