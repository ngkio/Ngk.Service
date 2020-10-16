using System.Collections.Generic;
using Thor.Framework.Business.Relational;
using Thor.Framework.Common.Pager;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;

namespace Ngk.Business.Interface
{
    public interface IConfigDataLogic : IBusinessLogic<ConfigData>
    {
        /// <summary>
        /// ����key��ȡvalue
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetByKey(string key);

        /// <summary>
        /// ʹ�����Ի�ȡ���� 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetByKeyAndLang(string key);

        /// <summary>
        /// ����key���ϻ�ȡ�������ݼ���
        /// </summary>
        /// <param name="keyList"></param>
        /// <returns></returns>
        List<ConfigData> GetByKeyList(List<string> keyList);

        /// <summary>
        /// ��ѯ-���
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IList<ConfigData> GetByParam(string key);
        
        /// <summary>
        /// ��ȡ�����б�(��ҳ)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        PagedResults<ConfigData> GetCconfigListQuery(ConfigDataParam param);

        /// <summary>
        /// ˢ�»���
        /// </summary>
        void RefreshMemoryCache();
    }
}

