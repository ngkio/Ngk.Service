using System;
using Thor.Framework.Data.Model;
using Ngk.Common;

namespace Ngk.DataAccess.DTO
{
    /// <summary>
    /// 更新节点请求参数
    /// </summary>
    public class UpdateNodeRequestModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string HttpAddress { get; set; }

        /// <summary>
        /// 连接方式
        /// </summary>
        public int ConnectType { get; set; }

        /// <summary>
        /// 是否是超级节点
        /// </summary>
        public bool IsSuper { get; set; }

        /// <summary>
        /// 超时
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
        /// 国家地区
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 参数检验
        /// </summary>
        public void Vefity()
        {
            if (Id == null || Id == Guid.Empty) throw new BusinessException(BusinessResultCode.ArgumentError, "参数错误或无效");

        }
    }
}
