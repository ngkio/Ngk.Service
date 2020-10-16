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
        /// ��ȡ�ڵ�
        /// </summary>
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
        /// �ڵ�Api����
        /// </summary>
        void ApiError();

        /// <summary>
        /// �ڵ�Api�쳣
        /// </summary>
        void ApiException();

        /// <summary>
        /// ��ҳ��ѯ�ڵ�����
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        PagedResults<Node> QueryNode(NodeParam model);
    }
}

