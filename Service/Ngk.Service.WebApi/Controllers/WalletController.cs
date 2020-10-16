using System;
using System.Collections.Generic;
using Thor.Framework.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ngk.Business.Interface;
using Ngk.Common;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities.Mongo;
using Ngk.Service.WebApi.Attribute;

namespace Ngk.Service.WebApi.Controllers
{
    /// <summary>
    /// 钱包控制器,该控制器下所有接口须进行签名认证
    /// 1、APP启动时调用接口config/GetTimestamp 记录当前时间与服务器的时间差
    /// 2、timestamp = 请求的当前时间-时间差
    /// 3、String data = 对参数进行排序转成json后拼接上timestamp
    /// 例：{"Account":"ogythwy4.ngk","PublicKey":"NGK7eGCvxx4eKimY5hsvaq92LZT5u6gCnzczetjVtNK2PwjQ5R76B","Uuid":"9764d7d59cb5439aa3b334d2af4ac720"}1592905241975
    /// 4、添加请求头addHeader("timestamp"，timestamp)
    /// 5、添加请求头addHeader("sign"，Sha512（data）)
    /// 例：sign: 93c0e3ceeef3750207e490f955c6f98794d1eb19c6d5bf13656c762e95dc9e534dce6ca4d5295009137aef88791000790819d3ae2e00e723eee8b5c12f926f15
    /// </summary>
    [Log]
    [Sign]
    [Language]
    [AllowAnonymous]
    [Route("wallet/[action]")]
    public class WalletController : Controller
    {
        private readonly ITransferRecordLogic _transferRecordLogic;
        private readonly IAccountLogic _accountLogic;
        private readonly IBlackContractLogic _blackContractLogic;

        public WalletController(ITransferRecordLogic transferRecordLogic, IAccountLogic accountLogic,
            IBlackContractLogic blackContractLogic)
        {
            _accountLogic = accountLogic;
            _transferRecordLogic = transferRecordLogic;
            _blackContractLogic = blackContractLogic;
        }

        /// <summary>
        /// 创建钱包帐号
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ExcutedResult CreateFreeAccount([FromBody] CreateFreeAccountRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return ExcutedResult.FailedResult(BusinessResultCode.ArgumentError, "参数无效");
                }
                var result = _accountLogic.CreateFreeAccount(model);
                return result;
            }
            catch (BusinessException bex)
            {
                return ExcutedResult.FailedResult(bex.ErrorCode, bex.Message);
            }
        }

        /// <summary>
        /// 添加提案
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ExcutedResult<ProposerResponse> AddProposal([FromBody] AddProposalRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return ExcutedResult<ProposerResponse>.FailedResult(BusinessResultCode.ArgumentError, "参数无效");
                }
                var result = _blackContractLogic.AddProposal(request);
                return ExcutedResult<ProposerResponse>.SuccessResult(result);
            }
            catch (BusinessException bex)
            {
                return ExcutedResult<ProposerResponse>.FailedResult(bex.ErrorCode, bex.Message);
            }
        }

        /// <summary>
        /// 更新提案Hash
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ExcutedResult UpdateProposalHash([FromBody] UpdateProposalRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return ExcutedResult.FailedResult(BusinessResultCode.ArgumentError, "参数无效");
                }
                return _blackContractLogic.UpdateProposalHash(request);
            }
            catch (BusinessException bex)
            {
                return ExcutedResult.FailedResult(bex.ErrorCode, bex.Message);
            }
        }

        /// <summary>
        /// 获取可用提案列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ExcutedResult<List<BlackContractModel>> QueryProposalList()
        {
            try
            {
                var list = _blackContractLogic.GetProposalList();
                return ExcutedResult<List<BlackContractModel>>.SuccessResult(list);
            }
            catch (BusinessException bex)
            {
                return ExcutedResult<List<BlackContractModel>>.FailedResult(bex.ErrorCode, bex.Message);
            }
        }

        /// <summary>
        /// 获取提案详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ExcutedResult<BlackContractModel> GetProposalDetail(long contractId)
        {
            try
            {
                var model = _blackContractLogic.GetProposalDetail(contractId);
                return ExcutedResult<BlackContractModel>.SuccessResult(model);
            }
            catch (BusinessException bex)
            {
                return ExcutedResult<BlackContractModel>.FailedResult(bex.ErrorCode, bex.Message);
            }
        }
    }
}