using Thor.Framework.Business.Relational;
using Thor.Framework.Data.Model;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.Entities;

namespace Ngk.Business.Interface
{
    public interface IContactsLogic : IBusinessLogic<Contacts>
    {
        /// <summary>
        /// 获取联系人
        /// </summary>
        /// <returns></returns>
        ExcutedResult GetContacts(ChainModel model);

        /// <summary>
        /// 添加联系人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExcutedResult AddContacts(AddContactsRequest model);


        /// <summary>
        /// 编辑联系人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
         ExcutedResult EditContacts(EditContactsRequest model);
    }
}

