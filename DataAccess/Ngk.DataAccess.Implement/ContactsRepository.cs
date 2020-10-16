using System;
using System.Collections.Generic;
using System.Linq;
using Thor.Framework.Data;
using Thor.Framework.Data.DbContext.Relational;
using Thor.Framework.Repository.Relational;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;

namespace Ngk.DataAccess.Implement
{
    public class ContactsRepository : BaseRepository<Contacts>, IContactsRepository
    {
        public ContactsRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// 获取联系人
        /// </summary>
        /// <returns></returns>
        public List<ContactsModel> GetContacts(Guid userId)
        {
            var contacts = DbSet.Where(p => p.State == (int)EnumState.Normal && p.UserId == userId);
            var account = DbContext.GetDbSet<ContactsAccount>().Where(p => p.State == (int)EnumState.Normal);
            var query = from c in contacts
                        join a in account on c.Id equals a.ContactsId
                        select new ContactsModel
                        {
                            Name = c.Name,
                            Mobile = c.Mobile,
                            Desc = c.Desc,
                            Id = c.Id,
                            ChainCode = a.ChainCode,
                            Account = a.Account,
                        };
            return query.ToList();
        }

        /// <summary>
        /// 获取用户相关的链联系帐号
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="chainCode"></param>
        /// <returns></returns>
        public List<string> GetContactsAccount(Guid userId,string chainCode)
        {
            var contacts = DbSet.Where(p => p.State == (int)EnumState.Normal && p.UserId == userId);
            var account = DbContext.GetDbSet<ContactsAccount>().Where(p => p.State == (int)EnumState.Normal&&p.ChainCode == chainCode);
            var query = from c in contacts
                join a in account on c.Id equals a.ContactsId
                select a.Account;
            return query.ToList();
        }
    }
}


