using System;
using Microsoft.Extensions.DependencyInjection;

namespace Contract.Interface
{
    public class ContractClientFactory
    {
        private readonly IServiceProvider _provider;

        public ContractClientFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public TOuter GetService<TOuter>(string chainCode) where TOuter : IBaseClient
        {
            foreach (var service in _provider.GetServices<TOuter>())
            {
                if (String.Equals(service.ChainCode, chainCode, StringComparison.CurrentCultureIgnoreCase)) return service;
            }

            return default(TOuter);
        }
    }
}