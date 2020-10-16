using System;
using System.Collections.Generic;
using Thor.Framework.Business.Relational;
using Thor.Framework.Data;
using Thor.Framework.Data.Model;
using Ngk.Business.Interface;
using Ngk.Common;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;

namespace Ngk.Business.Implement
{
    public class ContactsLogic : BaseBusinessLogic<Contacts, IContactsRepository>, IContactsLogic
    {
        private readonly IUserRepository _userRepository;
        private readonly IContactsAccountRepository _contactsAccountRepository;

        #region ctor
        public ContactsLogic(IContactsRepository repository, IUserRepository userRepository, IContactsAccountRepository contactsAccountRepository) : base(repository)
        {
            _userRepository = userRepository;
            _contactsAccountRepository = contactsAccountRepository;
        }
        #endregion

        public override void Create(Contacts entity, out ExcutedResult result)
        {
            entity.CreateTime = DateTime.UtcNow;
            base.Create(entity, out result);
        }

        public override void Delete(Guid id, out ExcutedResult result)
        {
            var entity = GetById(id);
            if (entity == null)
            {
                result = ExcutedResult.FailedResult(BusinessResultCode.ArgumentError, "数据不存在，请刷新");
                return;
            }

            entity.DeleteIp = CurrentUser.ClientIP;
            entity.State = (int)EnumState.Deleted;
            entity.DeleteTime = DateTime.UtcNow;
            entity.Deleter = CurrentUser.UserName;
            base.Update(entity, out result);
            result.Message = result.Status == EnumStatus.Success ? "删除成功" : "删除失败";
        }

        /// <summary>
        /// 获取联系人
        /// </summary>
        /// <returns></returns>
        public ExcutedResult GetContacts(ChainModel model)
        {
            var user = CurrentUser;
            var result = Repository.GetContacts(user.Id);
            return ExcutedResult.SuccessResult(result);
        }

        /// <summary>
        /// 添加联系人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExcutedResult AddContacts(AddContactsRequest model)
        {
            var user = CurrentUser;
            using (var trans = base.GetNewTransaction())
            {
                Contacts entity = new Contacts
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    Name = model.Name,
                    Desc = model.Desc,
                    Mobile = model.Mobile
                };
                Create(entity, out var excuted);
                foreach (var item in model.ContactAccounts)
                {
                    ContactsAccount account = new ContactsAccount
                    {
                        ContactsId = entity.Id,
                        Mobile = entity.Mobile,
                        Name = entity.Name,
                        ChainCode = item.ChainCode,
                        Account = item.Account,
                        CreateTime = DateTime.UtcNow,
                    };
                    _contactsAccountRepository.Add(account);
                }
                trans.Commit();
            }
            return ExcutedResult.SuccessResult();
        }

        /// <summary>
        /// 编辑联系人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExcutedResult EditContacts(EditContactsRequest model)
        {
            var entity = GetById(model.Id);
            using (var trans = base.GetNewTransaction())
            {
                entity.Name = model.Name;
                entity.Mobile = model.Mobile;
                entity.Desc = model.Desc;
                _contactsAccountRepository.Delete(p => p.ContactsId == entity.Id);
                List<ContactsAccount> accounts = new List<ContactsAccount>();
                foreach (var item in model.ContactAccounts)
                {
                    ContactsAccount account = new ContactsAccount
                    {
                        ContactsId = entity.Id,
                        Mobile = entity.Mobile,
                        Name = entity.Name,
                        ChainCode = item.ChainCode,
                        Account = item.Account,
                        CreateTime = DateTime.UtcNow,
                    };
                    accounts.Add(account);
                }
                _contactsAccountRepository.AddRange(accounts);
                trans.Commit();
            }
            return ExcutedResult.SuccessResult();
        }


    }
}


