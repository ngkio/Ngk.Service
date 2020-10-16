using Thor.Framework.Common.Pager;

namespace Ngk.DataAccess.DTO.Param
{
    /// <summary>
    /// 查询登录日志参数
    /// </summary>
    public  class LoginLogParam:AdvQueryParam
    {
        /// <summary>
        /// 电话号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 是否是管理员
        /// </summary>
        public int IsAdmin { get; set; }
    }
}
