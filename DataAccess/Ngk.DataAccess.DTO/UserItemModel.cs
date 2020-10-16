using System;

namespace Ngk.DataAccess.DTO
{
    public class UserItemModel
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Mobile { get; set; }

        public string Email { get; set; }

        /// <summary>
        /// 头像ID
        /// </summary>
        public Guid? AvatarId { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string Nickname { get; set; }
        public DateTime CreateTime { get; set; }

    }

    public class UserResourceModel : UserItemModel
    {
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 登录IP
        /// </summary>
        public string LogingIp { get; set; }

        /// <summary>
        /// 推荐人
        /// </summary>
        public string InviteUser { get; set; }

        /// <summary>
        /// 默认账号
        /// </summary>
        public string Account { get; set; }
    }

    
}
