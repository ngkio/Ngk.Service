using System.Collections.Generic;
using Thor.Framework.Common.Pager;
using Thor.Framework.Repository.Relational;
using Ngk.Common.Enum;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;

namespace Ngk.DataAccess.Interface
{
    public interface INodeRepository : IRepository<Node>
    {
        /// <summary>
        /// 获取节点
        /// </summary>
        /// <param name="type">节点类型</param>
        /// <returns>节点</returns>
        Node GetNode(string chainCode, EnumNodeType type);


        /// <summary>
        /// 获取节点合集
        /// </summary>
        /// <param name="chainCode"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        List<Node> GetNodeList(string chainCode, EnumNodeType type);

        /// <summary>
        /// 节点Api错误
        /// </summary>
        void ApiError();

        /// <summary>
        /// 节点Api异常
        /// </summary>
        void ApiException();

        /// <summary>
        /// 分页查询节点数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        PagedResults<Node> QueryNode(NodeParam model);
    }
}

