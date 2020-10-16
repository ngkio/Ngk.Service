using Thor.Framework.Business.Relational;
using Thor.Framework.Data.Model;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.Entities;

namespace Ngk.Business.Interface
{
    public interface IContactsLogic : IBusinessLogic<Contacts>
    {
        /// <summary>
        /// ��ȡ��ϵ��
        /// </summary>
        /// <returns></returns>
        ExcutedResult GetContacts(ChainModel model);

        /// <summary>
        /// �����ϵ��
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExcutedResult AddContacts(AddContactsRequest model);


        /// <summary>
        /// �༭��ϵ��
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
         ExcutedResult EditContacts(EditContactsRequest model);
    }
}

