using System.Threading.Tasks;
using Contract.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ngk.DataAccess.DTO;
using Ngk.Service.WebApi.Attribute;
using Thor.Framework.Data.Model;
using Thor.Framework.Ext.NGKSharp;
using Thor.Framework.Ext.NGKSharp.Core;
using Thor.Framework.Ext.NGKSharp.Core.Api.v1;
using Thor.Framework.Ext.NGKSharp.Core.Providers;

namespace Ngk.Service.WebApi.Controllers
{
    [AllowAnonymous]
    [Route("chain/[action]")]
    public class ChainController: Controller
    {
        private readonly IInfoClient _infoClient;
        
        public ChainController(IInfoClient infoClient)
        {
            _infoClient = infoClient;
        }

        [HttpPost]
        public async Task<ExcutedResult> TransactionBinToJson([FromBody] TrxBinToJsonRequest request)
        {
            return await _infoClient.TransactionBinToJson(request.HexData);
        }
    }
}