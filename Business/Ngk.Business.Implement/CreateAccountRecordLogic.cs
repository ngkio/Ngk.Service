using Thor.Framework.Business.Relational;
using Ngk.Business.Interface;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;

namespace Ngk.Business.Implement
{
    public class CreateAccountRecordLogic : BaseBusinessLogic<CreateAccountRecord, ICreateAccountRecordRepository>, ICreateAccountRecordLogic
    {
        #region ctor
        public CreateAccountRecordLogic(ICreateAccountRecordRepository repository) : base(repository)
        {

        }
        #endregion
    }
}


