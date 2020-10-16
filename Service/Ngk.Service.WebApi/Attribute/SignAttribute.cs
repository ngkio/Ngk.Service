using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AspectCore.Injector;
using Thor.Framework.Common.Helper.Extensions;
using Thor.Framework.Common.IoC;
using Thor.Framework.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Newtonsoft.Json;
using Ngk.Business.Interface;
using Ngk.Common;
using Ngk.DataAccess.Entities.Mongo;
using Ngk.DataAccess.Interface.Mongo;
using Thor.Framework.Data;

namespace Ngk.Service.WebApi.Attribute
{
    public class SignAttribute : ActionFilterAttribute
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var request = context.HttpContext.Request;
            if (request.Method == "POST")
            {
                IHttpContextAccessor httpContextAccessor = AspectCoreContainer.CreateScope().Resolve<IHttpContextAccessor>();
                var configDataLogic = AspectCoreContainer.CreateScope().Resolve<IConfigDataLogic>();
                var ip = httpContextAccessor.HttpContext.GetRequestIp4Address()?.ToString();
                var ipWhiteList = configDataLogic.GetByKey(ConfigDataKey.IpWhiteList);
                var timestampOffsetMinute = configDataLogic.GetByKey(ConfigDataKey.TimestampOffsetMinute);
                double.TryParse(timestampOffsetMinute, out var minute);
                var actionParams = StreamHelper.GetStreamText(request.Body);
                Dictionary<string, object> jsonDict = JsonConvert.DeserializeObject<Dictionary<string, Object>>(actionParams);
                var d = new SortedDictionary<string, object>(jsonDict);
                var sss = JsonConvert.SerializeObject(d);
                var timeHeader = request.Headers["timestamp"].ToString();
                var signHeader = request.Headers["sign"].ToString();
                if (timeHeader == "" || signHeader == "")
                {
                    context.Result = new JsonResult(ExcutedResult.FailedResult(BusinessResultCode.NoSign, "调用错误"));
                }
                else
                {
                    if (long.TryParse(timeHeader, out var timestamp))
                    {
                        var time = DateTimeHelper.ConvertFromTimeStamp(timestamp);
                        if (time == null || time.Value.AddMinutes(minute).ToUniversalTime() < DateTime.UtcNow)
                        {
                            context.Result = new JsonResult(ExcutedResult.FailedResult(BusinessResultCode.NoSign, "调用错误"));
                        }
                        var sign = ShaHelper.Encrypt(sss + timeHeader);
                        if (sign != signHeader)
                        {
                            context.Result = new JsonResult(ExcutedResult.FailedResult(BusinessResultCode.SignError, "签名错误"));
                        }
                    }
                    else
                    {
                        context.Result = new JsonResult(ExcutedResult.FailedResult(BusinessResultCode.NoSign, "调用错误"));
                    }
                }


                if (!string.IsNullOrEmpty(ipWhiteList))
                {
                    if (!string.IsNullOrEmpty(ip) && !ipWhiteList.Contains(ip))
                    {
                        context.Result =
                            new JsonResult(ExcutedResult.FailedResult(SysResultCode.ServerException, "Your ip not access"));
                    }
                }
            }

            return base.OnActionExecutionAsync(context, next);
        }
    }
}