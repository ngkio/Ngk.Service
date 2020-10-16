using Thor.Framework.Common.Pager;

namespace Ngk.DataAccess.DTO.Param
{
    /// <summary>
    /// 操作日志查询
    /// </summary>
    public class OperateLogParams : AdvQueryParam
    {
        /// <summary>
        /// 账户
        /// </summary>
        public string ManagerAccount { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public string Operate { get; set; }
        
        /// <summary>
        /// IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 功能
        /// </summary>
        public string Function { get; set; }
    }
}
