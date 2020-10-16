using System;
using Thor.Framework.Data.DbContext.Relational;
using Thor.Framework.Repository.Relational;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;

namespace Ngk.DataAccess.Implement
{
    public class ContactsAccountRepository : BaseRepository<ContactsAccount>, IContactsAccountRepository
    {
        public ContactsAccountRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="contactsId"></param>
        public void DeleteByContactsId(Guid contactsId)
        {            
            Delete(p => p.ContactsId == contactsId);
        }
    }
}


