using System;
using System.Threading.Tasks;
using AspectCore.Injector;
using Thor.Framework.Common.IoC;
using Thor.Framework.Data;
using Thor.Framework.Data.Model;
using Microsoft.AspNetCore.Mvc.Filters;
using Ngk.Common;
using Ngk.Common.Enum;
using Ngk.DataAccess.Interface;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace Ngk.Service.WebApi.Attribute
{
    public class LanguageAttribute : ActionFilterAttribute
    {

        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is Microsoft.AspNetCore.Mvc.ObjectResult)
            {
                var httpContext = context.HttpContext;
                ILanguageRepository languageRepository = AspectCoreContainer.CreateScope().Resolve<ILanguageRepository>();
                var httpResult = (Microsoft.AspNetCore.Mvc.ObjectResult)context.Result;

                if (httpResult != null && httpResult.Value != null && httpResult.Value is ExcutedResult)
                {
                    var resultDataInfo = httpResult.Value as ExcutedResult;
                    //不是成功
                    if (resultDataInfo.Code != SysResultCode.Success)
                    {
                        var code = languageRepository.GetDescribe(resultDataInfo.Code,
                            EnumLanguageDataType.BusinessResultCode, LanguageHelper.GetEnumLanguageType(httpContext));
                        if (!String.IsNullOrEmpty(code))
                        {
                            resultDataInfo.Message = code;
                        }
                        httpResult.Value = resultDataInfo;
                        context.Result = httpResult;
                    }
                }
                else if (httpResult != null && httpResult.Value != null && httpResult.Value.ToString().Contains("ExcutedResult"))
                {
                    ExcutedResult<object> excutedResult =
                        JsonConvert.DeserializeObject<ExcutedResult<object>>(JsonConvert.SerializeObject(httpResult.Value));

                    if (excutedResult.Code != SysResultCode.Success)
                    {
                        var code = languageRepository.GetDescribe(excutedResult.Code,
                            EnumLanguageDataType.BusinessResultCode, LanguageHelper.GetEnumLanguageType(httpContext));
                        if (!String.IsNullOrEmpty(code))
                        {
                            excutedResult.Message = code;
                        }
                        httpResult.Value = excutedResult;
                        context.Result = httpResult;
                    }
                }
            }

            return base.OnResultExecutionAsync(context, next);
        }
    }
}
