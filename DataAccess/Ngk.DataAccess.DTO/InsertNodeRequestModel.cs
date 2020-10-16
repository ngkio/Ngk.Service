using Thor.Framework.Data.Model;
using Ngk.Common;

namespace Ngk.DataAccess.DTO
{
    public class InsertNodeRequestModel
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string HttpAddress { get; set; }
        
        /// <summary>
        /// 是否是超级节点
        /// </summary>
        public bool IsSuper { get; set; }

        /// <summary>
        /// 国家地区
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 错误次数
        /// </summary>
        public int ErrorCount { get; set; }

        /// <summary>
        /// 超时时间
        /// </summary>
        public int TimeOut { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 查询备选
        /// </summary>
        public bool QueryAlternative { get; set; }

        /// <summary>
        /// 玩家备选
        /// </summary>
        public bool PlayerAlternative { get; set; }

        /// <summary>
        /// 服务备选
        /// </summary>
        public bool ServerAlternative { get; set; }

        /// <summary>
        /// 参数验证
        /// </summary>
        public void Vefity()
        {
            if (string.IsNullOrEmpty(Name)) throw new BusinessException(BusinessResultCode.ArgumentError, "参数错误或无效");

            if (string.IsNullOrEmpty(Address)) throw new BusinessException(BusinessResultCode.ArgumentError, "参数错误或无效");

            if (string.IsNullOrEmpty(HttpAddress)) throw new BusinessException(BusinessResultCode.ArgumentError, "参数错误或无效");
        }
    }
}