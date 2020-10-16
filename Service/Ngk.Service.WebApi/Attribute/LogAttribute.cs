using System;
using System.Threading.Tasks;
using AspectCore.Injector;
using Thor.Framework.Common.Helper.Extensions;
using Thor.Framework.Common.IoC;
using Thor.Framework.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Newtonsoft.Json;
using Ngk.Common;
using Ngk.DataAccess.Entities.Mongo;
using Ngk.DataAccess.Interface.Mongo;

namespace Ngk.Service.WebApi.Attribute
{
    public class LogAttribute : ActionFilterAttribute
    {
        private static readonly string key = "enterTime";

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //记录进入请求的时间
            context.HttpContext.Items[key] = DateTime.UtcNow;
            return base.OnActionExecutionAsync(context, next);
        }

        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            try
            {
                IHttpContextAccessor httpContextAccessor = AspectCoreContainer.CreateScope().Resolve<IHttpContextAccessor>();
                IMonitorLogRepository monitorLogRepository = AspectCoreContainer.CreateScope().Resolve<IMonitorLogRepository>();
                DateTime time = (DateTime)context.HttpContext.Items[key];
                var request = context.HttpContext.Request;
                var headerStr = JsonConvert.SerializeObject(request.Headers);
                MonitorLog monLog = new MonitorLog()
                {
                    ExecuteStartTime = time,
                    ExecuteEndTime = DateTime.UtcNow,
                    IP = httpContextAccessor.HttpContext.GetRequestIp4Address()?.ToString(),
                    ClientAgent = ((HttpRequestHeaders)((DefaultHttpRequest)request).Headers).HeaderUserAgent,
                    Path = (request.Path.HasValue ? request.Path.ToString() : "") + (request.QueryString.HasValue ? request.QueryString.ToString() : ""),
                    ActionParams = StreamHelper.GetStreamText(request.Body),
                    HttpRequestHeaders = headerStr,
                    HttpMethod = request.Method,
                    Response = JsonConvert.SerializeObject(context.Result)
                };
                monLog.Elapsed = monLog.CostTime;
                monitorLogRepository.Insert(monLog);
                //Log4NetHelper.WriteDebug(GetType(), monLog.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return base.OnResultExecutionAsync(context, next);
        }

    }
}