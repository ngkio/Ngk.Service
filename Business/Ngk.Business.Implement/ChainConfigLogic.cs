using Thor.Framework.Business.Relational;
using Ngk.Business.Interface;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;

namespace Ngk.Business.Implement
{
    public class ChainConfigLogic : BaseBusinessLogic<ChainConfig, IChainConfigRepository>, IChainConfigLogic
    {
        #region ctor
        public ChainConfigLogic(IChainConfigRepository repository) : base(repository)
        {

        }
        #endregion
        
    }
}


