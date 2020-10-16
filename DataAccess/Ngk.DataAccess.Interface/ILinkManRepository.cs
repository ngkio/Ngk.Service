using System;
using System.Collections.Generic;
using Thor.Framework.Repository.Relational;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.Entities;

namespace Ngk.DataAccess.Interface
{
    public interface IContactsRepository : IRepository<Contacts>
    {
        /// <summary>
        /// 获取联系人
        /// </summary>
        /// <returns></returns>
        List<ContactsModel> GetContacts(Guid userId);

        /// <summary>
        /// 获取用户相关的链联系帐号
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="chainCode"></param>
        /// <returns></returns>
        List<string> GetContactsAccount(Guid userId, string chainCode);
    }
}

