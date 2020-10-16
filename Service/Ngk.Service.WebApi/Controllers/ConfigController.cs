using System;
using System.Collections.Generic;
using System.Linq;
using Thor.Framework.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ngk.Business.Interface;
using Ngk.Common;
using Ngk.Common.Enum;
using Ngk.DataAccess.DTO;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;
using Ngk.Service.WebApi.Attribute;

namespace Ngk.Service.WebApi.Controllers
{
    /// <summary>
    /// 配置控制器
    /// </summary>
    [AllowAnonymous]
    [Language]
    [Route("config/[action]")]
    public class ConfigController : Controller
    {
        private readonly IChainConfigRepository _chainConfigRepository;
        private readonly IVersionLogic _versionLogic;
        private readonly IConfigDataLogic _configDataLogic;
        private readonly INodeLogic _nodeLogic;
        private readonly string _filePreUrl;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ConfigController(IChainConfigRepository chainConfigRepository, IConfiguration configuration,
            IVersionLogic versionLogic, IConfigDataLogic configDataLogic, INodeLogic nodeLogic, IHttpContextAccessor httpContextAccessor)
        {
            _chainConfigRepository = chainConfigRepository;
            _versionLogic = versionLogic;
            _configDataLogic = configDataLogic;
            _nodeLogic = nodeLogic;
            _filePreUrl = configuration["FilePreUrl"];
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 获取当前版本
        /// </summary>
        /// <param name="clientType">客户端类型，1、Web，2、IOS,3、Android</param>
        /// <returns></returns>
        [HttpGet]
        public ExcutedResult<VersionModel> GetCurrentVersion(int clientType)
        {
            try
            {
                if (!Enum.IsDefined(typeof(EnumClientType), clientType))
                {
                    return ExcutedResult<VersionModel>.FailedResult(BusinessResultCode.ArgumentError, "参数错误或无效");
                }
                var version = _versionLogic.GetCurrentVersion(clientType);
                if (version == null)
                {
                    return ExcutedResult<VersionModel>.SuccessResult();
                }
                var anUrl = _configDataLogic.GetByKeyAndLang(ConfigDataKey.AndroidDownUrl);
                var iosUrl = _configDataLogic.GetByKeyAndLang(ConfigDataKey.IosDownUrl);
                var data = new VersionModel
                {
                    Name = version.Name,
                    Version = version.Number.Value,
                    Introduce = version.Desc,
                    IsForce = version.IsMustUpdate,
                    DownUrl = (clientType == (int)EnumClientType.Android) ? anUrl : ((clientType == (int)EnumClientType.Ios) ? iosUrl : "")
                };
                return ExcutedResult<VersionModel>.SuccessResult(data);
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult<VersionModel>.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }

        /// <summary>
        /// 获取可用链列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ExcutedResult GetChainList()
        {
            try
            {
                var list = _chainConfigRepository.GetAll();
                var data = list.Select(p =>
                    new { p.ChainCode, p.ChainId, Logo = p.Logo.HasValue ? _filePreUrl + p.Logo.Value : "" });
                return ExcutedResult.SuccessResult(data);
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }

        /// <summary>
        /// 获取节点列表
        /// </summary>
        /// <param name="chainCode"></param>
        /// <returns></returns>
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ExcutedResult GetNodeList(string chainCode)
        {
            try
            {
                if (string.IsNullOrEmpty(chainCode))
                {
                    return ExcutedResult.FailedResult(BusinessResultCode.ArgumentError, "参数错误或无效");
                }
                var list = _nodeLogic.GetNodeList(chainCode, EnumNodeType.Player);
                var data = list.Select(p => new { p.Name, p.HttpAddress, p.Priority }
                );
                return ExcutedResult.SuccessResult(data);
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }

        /// <summary>
        /// 获取玩家节点
        /// </summary>
        /// <param name="chainCode">链，必传</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ExcutedResult GetNodeListForPlayer(string chainCode)
        {
            try
            {
                if (string.IsNullOrEmpty(chainCode))
                {
                    return ExcutedResult.FailedResult(BusinessResultCode.ArgumentError, "参数错误或无效");
                }
                var result = _nodeLogic.GetNodeList(chainCode, EnumNodeType.Player);
                return ExcutedResult.SuccessResult(result);
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }

        /// <summary>
        /// 获取配置数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public ExcutedResult GetValue(string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    return ExcutedResult.FailedResult(BusinessResultCode.ArgumentError, "参数错误或无效");
                }
                var value = _configDataLogic.GetByKeyAndLang(key);
                return ExcutedResult.SuccessResult(value, value);
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }

        /// <summary>
        /// 获取全部配置数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ExcutedResult GetAllConfig()
        {
            try
            {
                var list = _configDataLogic.GetByParam("").Where(p => p.IsApp);
                var langStrs = LanguageHelper.GetForeignLangStrList();
                var langStr = LanguageHelper.GetStringLanguageType(_httpContextAccessor.HttpContext);
                //语言相关的配置集合
                List<ConfigData> langConfig = list.ToList();
                if (list.Any())
                {
                    foreach (var configData in list)
                    {
                        if (!String.IsNullOrEmpty(configData.ConfigKey) && configData.ConfigKey.Length > 3)
                        {
                            var key = configData.ConfigKey.Trim().Substring(0, configData.ConfigKey.Length - 3);
                            var lastThree = configData.ConfigKey.Trim().Substring(configData.ConfigKey.Length - 3, 1);
                            var lastTwo = configData.ConfigKey.Trim().Substring(configData.ConfigKey.Length - 2);
                            if (lastThree == "_" && langStrs.Contains(lastTwo.ToLower()))
                            {
                                if (langStr == lastTwo)
                                {
                                    var configDataFir = langConfig.FirstOrDefault(p => p.ConfigKey == key);
                                    if (configDataFir != null)
                                    {
                                        configDataFir.ConfigValue = configData.ConfigValue;
                                    }
                                }
                                langConfig.Remove(configData);
                            }
                        }
                    }
                }
                var configs = langConfig.Select(p => new
                {
                    key = p.ConfigKey,
                    value = p.ConfigValue
                });
                return ExcutedResult.SuccessResult(configs);
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }

        /// <summary>
        /// 刷新缓存
        /// </summary>
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ExcutedResult RefreshMemoryCache()
        {
            try
            {
                _configDataLogic.RefreshMemoryCache();
                return ExcutedResult.SuccessResult();
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }


        /// <summary>
        /// 获取当前服务时间戳
        /// </summary>
        [HttpGet]
        public ExcutedResult GetTimestamp()
        {
            try
            {
                return ExcutedResult.SuccessResult(DateTimeHelper.GetTimeStamp(DateTime.UtcNow, true));
            }
            catch (BusinessException businessException)
            {
                return ExcutedResult.FailedResult(businessException.ErrorCode, businessException.Message);
            }
        }
    }
}
