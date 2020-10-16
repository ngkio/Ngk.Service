using System;

namespace Ngk.DataAccess.DTO
{
   public  class InviteModel
    {
        /// <summary>
        /// 用户电话
        /// </summary>
        public string UserMobile { get; set; }

        /// <summary>
        /// 用户EOS帐号
        /// </summary>
        public string UserAccount { get; set; }

        /// <summary>
        /// 邀请码,如无邀请人则为空
        /// </summary>
        public string InviteCode { get; set; }

        /// <summary>
        /// 邀请人电话,如无邀请人则为空
        /// </summary>
        public string InviteMobile { get; set; }

        /// <summary>
        /// 邀请人EOS帐号,如无邀请人则为空
        /// </summary>
        public string InviteAccount { get; set; }


        /// <summary>
        /// 邀请人EOS帐号,如无邀请人则为空
        /// </summary>
        public DateTime? CreateTime { get; set; }

        
    }

    /// <summary>
    /// 用户推荐次数
    /// </summary>
    public class InviteCountModel : InviteModel
    {

        /// <summary>
        /// 推荐次数
        /// </summary>
        public int InviteCount { get; set; }

        public Guid InviteUserId;
    }
}
