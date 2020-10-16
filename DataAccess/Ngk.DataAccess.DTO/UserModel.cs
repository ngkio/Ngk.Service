using System;

namespace Ngk.DataAccess.DTO
{
    public class UserModel
    {
        /// <summary>用户姓名</summary>
        public string NickName { get; set; }

        /// <summary>手机号</summary>
        public string Mobile { get; set; }

        /// <summary>头像</summary>
        public string Avatar { get; set; }

        /// <summary>用户Id(主键唯一标识)</summary>
        public Guid Id { get; set; }

        public string DefaultAccount { get; set; }

        /// <summary>
        /// 邀请码
        /// </summary>
        public string InviteCode { get; set; }

        /// <summary>
        /// 邀请注册地址
        /// </summary>
        public string InviteRegistUrl { get; set; }
    }
}