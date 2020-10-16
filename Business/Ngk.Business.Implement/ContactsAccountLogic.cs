using Thor.Framework.Business.Relational;
using Ngk.Business.Interface;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;

namespace Ngk.Business.Implement
{
    public class ContactsAccountLogic : BaseBusinessLogic<ContactsAccount, IContactsAccountRepository>, IContactsAccountLogic
    {
        #region ctor
        public ContactsAccountLogic(IContactsAccountRepository repository) : base(repository)
        {

        }
        #endregion
    }
}


