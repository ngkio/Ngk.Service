using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ngk.DataAccess.DTO
{
    public class KeyRequest
    {
        /// <summary>
        /// 链Code
        /// </summary>
        [Required]
        public string ChainCode { get; set; }

        /// <summary>
        /// PublicKey公钥
        /// </summary>
        [Required]
        public string PublicKey { get; set; }
    }


    public class ImportAccountRequest : KeyRequest
    {
        /// <summary>
        /// 帐号名
        /// </summary>
        public List<AnalysisKeyInfo> Accounts { get; set; }
    }
}
