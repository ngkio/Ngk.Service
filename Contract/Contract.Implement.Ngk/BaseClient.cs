using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Ngk.Common;
using Ngk.Common.Enum;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;
using Thor.Framework.Common.Helper;
using Thor.Framework.Common.IoC;
using Thor.Framework.Data.Model;
using Thor.Framework.Ext.NGKSharp.Core;
using Thor.Framework.Ext.NGKSharp.Core.Api.v1;
using Thor.Framework.Ext.NGKSharp.Core.Exceptions;

namespace Contract.Implement.Ngk
{
    public abstract class BaseClient : IBaseClient
    {
        protected readonly Thor.Framework.Ext.NGKSharp.Ngk Client;
        protected readonly INodeRepository NodeRepository;
        protected readonly IConfigDataRepository ConfigDataRepository;
        protected readonly IChainConfigRepository ChainConfigRepository;
        protected readonly IConfiguration Configuration;
        protected ChainConfigurator Configurator;
        protected Node Point;

        public string ChainCode => "Ngk";

        protected BaseClient()
        {
            var scope = AspectCoreContainer.CreateScope();
            NodeRepository = (INodeRepository)scope.Resolve(typeof(INodeRepository));
            ConfigDataRepository = (IConfigDataRepository)scope.Resolve(typeof(IConfigDataRepository));
            ChainConfigRepository = (IChainConfigRepository)scope.Resolve(typeof(IChainConfigRepository));
            Configuration = (IConfiguration)scope.Resolve(typeof(IConfiguration));
            var chain = ChainConfigRepository.GetByChainCode(ChainCode);
            Point = NodeRepository.GetNode(ChainCode, EnumNodeType.Server);
            Configurator = new ChainConfigurator
            {
                HttpEndpoint = Point.HttpAddress,
                ChainId = chain.ChainId,
                ExpireSeconds = Point.TimeOut,
            };
            Client = new Thor.Framework.Ext.NGKSharp.Ngk(Configurator);
        }

        protected virtual async Task<ExcutedResult<string>> PushAction(Action action)
        {
            var trans = new Transaction
            {
                actions = new List<Action>
                {
                    action
                }
            };

            try
            {
                var result = await Client.CreateTransaction(trans);

                if (string.IsNullOrEmpty(result))
                {
                    return ExcutedResult<string>.FailedResult(BusinessResultCode.DataNotExist, "数据不存在，请刷新!.");
                }

                return ExcutedResult<string>.SuccessResult("", result);
            }
            catch (ApiErrorException e)
            {
                Log4NetHelper.WriteError(GetType(), e, $"Point:{Point.HttpAddress} Code:{e.code} ErrorName:{e.error.name} Error:{e.error.what} \nAction:{JsonConvert.SerializeObject(action)}");
                return ExcutedResult<string>.FailedResult(BusinessResultCode.ChainRequestApiError, "EOS request api error.");
            }
            catch (ApiException ex)
            {
                NodeRepository.ApiException();
                Log4NetHelper.WriteError(GetType(), ex, $"Point:{Point.HttpAddress} StatusCode:{ex.StatusCode} Content:{ex.Content}");
                return ExcutedResult<string>.FailedResult(BusinessResultCode.ChainRequestError, "EOS request error.");
            }
        }

        public virtual void SwitchNode()
        {
            Point = NodeRepository.GetNode(ChainCode, EnumNodeType.Server);
        }
    }
}