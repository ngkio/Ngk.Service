using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thor.Framework.Business.Relational;
using Thor.Framework.Data.Model;
using Ngk.Common.Enum;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;

namespace Ngk.Business.Interface
{
    public interface INodeLogic : IBusinessLogic<Node>
    {
        /// <summary>
        /// 获取节点
        /// </summary>
        /// <param name="chainCode"></param>
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
        /// 更新错误数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ExcutedResult AddErrorCount(Guid id);


        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExcutedResult QueryNodeInfos(NodeParam model);

        /// <summary>
        /// 插入新的节点
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExcutedResult InsertNode(InsertNodeRequestModel model);

        /// <summary>
        /// 更新节点信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExcutedResult UpdateNode(UpdateNodeRequestModel model);

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        ExcutedResult DeleteNode(String nodeId);


        /// <summary>
        /// 测试接口速度
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ExcutedResult> GetSpeed(Guid id);
    }

}

