using System;
using Thor.Framework.Common.Pager;

namespace Ngk.DataAccess.DTO.Param
{
   public  class InviteParam : AdvQueryParam
    {
        /// <summary>
        ///推荐人手机号
        /// </summary>
        public string InviteMobile   { get; set; }
        /// <summary>
        /// 推荐人邀请码
        /// </summary>
        public string InviteCode { get; set; }

        /// <summary>
        /// id
        /// </summary>
        public Guid? InviteuserId { get; set; }

        /// <summary>
        /// 被邀请人电话号
        /// </summary>
        public string UserMobile { get; set; }
        

    }
}
