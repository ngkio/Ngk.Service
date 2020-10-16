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
        /// ��ȡ�ڵ�
        /// </summary>
        /// <param name="chainCode"></param>
        /// <param name="type">�ڵ�����</param>
        /// <returns>�ڵ�</returns>
        Node GetNode(string chainCode, EnumNodeType type);

        /// <summary>
        /// ��ȡ�ڵ�ϼ�
        /// </summary>
        /// <param name="chainCode"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        List<Node> GetNodeList(string chainCode, EnumNodeType type);

        /// <summary>
        /// ���´�����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ExcutedResult AddErrorCount(Guid id);


        /// <summary>
        /// ��ҳ��ѯ����
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExcutedResult QueryNodeInfos(NodeParam model);

        /// <summary>
        /// �����µĽڵ�
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExcutedResult InsertNode(InsertNodeRequestModel model);

        /// <summary>
        /// ���½ڵ���Ϣ
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExcutedResult UpdateNode(UpdateNodeRequestModel model);

        /// <summary>
        /// ɾ���ڵ�
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        ExcutedResult DeleteNode(String nodeId);


        /// <summary>
        /// ���Խӿ��ٶ�
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ExcutedResult> GetSpeed(Guid id);
    }

}

