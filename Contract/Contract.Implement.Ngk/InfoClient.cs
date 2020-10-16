using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Contract.Implement.Ngk.Model;
using Contract.Interface;
using Thor.Framework.Common.Helper;
using Thor.Framework.Data;
using Thor.Framework.Data.Model;
using Ngk.Common;
using Thor.Framework.Ext.NGKSharp;
using Thor.Framework.Ext.NGKSharp.Core.Api.v1;
using Thor.Framework.Ext.NGKSharp.Core.Exceptions;
using Thor.Framework.Ext.NGKSharp.Core.Providers;

namespace Contract.Implement.Ngk
{
    public class InfoClient : BaseClient, IInfoClient
    {
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        public async Task<ExcutedResult> GetInfo()
        {
            try
            {
                var result = await Client.GetInfo();
                if (result == null)
                {
                    return ExcutedResult.FailedResult(BusinessResultCode.ChainRequestError, "Chain request  error");
                }

                return ExcutedResult.SuccessResult(result);
            }
            catch (ApiException ex)
            {
                NodeRepository.ApiException();
                Log4NetHelper.WriteError(typeof(InfoClient), ex,
                    $"Point:{Point.HttpAddress} StatusCode:{ex.StatusCode} Content:{ex.Content}");
                return ExcutedResult.FailedResult(BusinessResultCode.ChainRequestError, "Chain request  error.");
            }
            catch (Exception ex)
            {
                Log4NetHelper.WriteError(GetType(), ex, $"Point:{Point.HttpAddress}");
                return ExcutedResult.FailedResult(SysResultCode.ServerException, "");
            }
        }


        /// <summary>
        /// 获取1kbEos价格
        /// </summary>
        /// <returns></returns>
        public async Task<decimal> GetRamEosPrice(decimal n, int retryTime = 3)
        {
            try
            {
                var result = await Client.GetTableRows<RamMarket>(new GetTableRowsRequest()
                {
                    json = true,
                    code = "eosio",
                    scope = "eosio",
                    table = "rammarket",
                    limit = 1
                });
                var data = result.rows?.FirstOrDefault();
                if (data == null)
                {
                    return 0;
                }

                var price = (n * data.Quote.GetBalanceValue()) / (n + data.Base.GetBalanceValue() / 1024);
                return price;
            }
            catch (Exception ex)
            {
                if (retryTime <= 0)
                    return 0;
                SwitchNode();
                Log4NetHelper.WriteError(GetType(), ex, $"Point:{Point.HttpAddress} retryTime:{retryTime}");
                return await GetRamEosPrice(n, --retryTime);
            }
        }

        
        public async Task<ExcutedResult> TransactionBinToJson(string hexData)
        {
            var abiSer = new AbiSerializationProvider(new NgkApi(Configurator, new HttpHandler()));

            var transaction = await abiSer.DeserializePackedTransaction(hexData);

            return ExcutedResult.SuccessResult(transaction);
        }

        /// <summary>
        /// 测试节点速度
        /// </summary>
        /// <returns>测速结果</returns>
        public async Task<ExcutedResult> TestNetSpeed(int time, string url)
        {
            double elapsed = 0;
            Stopwatch sw = new Stopwatch();
            try
            {
                sw.Start();
                await Client.GetInfo();
                sw.Stop();
                elapsed = sw.Elapsed.TotalMilliseconds;
            }
            catch (Exception)
            {
                sw.Stop();
                return ExcutedResult.FailedResult(SysResultCode.ServerException, "");
            }

            return ExcutedResult.SuccessResult(elapsed);
        }
    }
}