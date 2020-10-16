using System;

namespace Ngk.Service.ManagerWebApi.Models
{
    /// <summary>
    /// 更新密码实体
    /// </summary>
    public class UpdatePassWordModel
    {
        /// <summary>
        /// 账户名
        /// </summary>
        //public String UserName { get; set; }

        /// <summary>
        /// 旧密码
        /// </summary>
        public String OldPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        public String NewPassword { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        public String ConfirmPassword { get; set; }
    }
}
