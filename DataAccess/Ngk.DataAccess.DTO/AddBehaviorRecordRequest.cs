using System;
using System.ComponentModel.DataAnnotations;

namespace Ngk.DataAccess.DTO
{
    public class AddBehaviorRecordRequest
    {
        /// <summary>
        /// DAPP ID
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// 进入帐号
        /// </summary>
        [Required]
        public string Account { get; set; }
    }
}
