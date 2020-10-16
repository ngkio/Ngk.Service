using AspectCore.Extensions.DependencyInjection;
using Contract.Interface;
using log4net;
using log4net.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ngk.Business.Implement.Captcha;
using Ngk.Business.Implement.Captcha.Tencent;
using Ngk.Business.Implement.Sms;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Implement.Mongo;
using Ngk.Service.ManagerWebApi.Common;
using System;
using System.Net;
using System.Threading.Tasks;
using Thor.Framework.Common.Helper;
using Thor.Framework.Common.IoC;
using Thor.Framework.Common.Options;
using Thor.Framework.Data.DbContext.Mongo;
using Thor.Framework.Data.DbContext.Relational;
using Thor.Framework.Data.Model;
using Thor.Framework.Service.WebApi.Middleware;

namespace Ngk.Service.ManagerWebApi
{
    public class Startup
    {
        private static ILoggerRepository Repository { get; set; }

        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
            Environment = env;
            //初始化log4net
            Repository = LogManager.CreateRepository("LTRepository");
            Log4NetHelper.SetConfig(Repository, $"log4net.{env.EnvironmentName}.config");
        }

        private IConfiguration Configuration { get; }
        private IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            #region 缓存

            services.AddMemoryCache();
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration["Redis:ConnectionString"];//redis连接字符串
                options.InstanceName = Configuration["Redis:Instance"];//Redis实例名称
            });
            #endregion


            #region 注入Database

            var settings = Configuration.Get<CustomerSettings>();

            foreach (var dbSetting in settings.DatabaseSettings)
            {
                switch (dbSetting.DatabaseType)
                {
                    case "MsSqlServer":
                        services.AddDbContext<IDbContextCore, SqlServerDbContext>(ServiceLifetime.Scoped); //注入EF上下文
                        break;
                    case "MySql":
                        services.AddDbContext<IDbContextCore, CoreServiceContext>(ServiceLifetime.Scoped); //注入EF上下文
                        break;
                    case "MongoDB":
                        services.AddScoped<IMongoDbContext, MongoDbContext>(p => new MongoDbContext(dbSetting)); //注入EF上下文
                        continue;
                    case "ExchangeMongoDb":
                        services.AddScoped<ExchangeMongoDbContext>(p => new ExchangeMongoDbContext(dbSetting)); //注入EF上下文
                        continue;
                    case "PanguMongoDbContext":
                        services.AddScoped<PanguMongoDbContext>(p => new PanguMongoDbContext(dbSetting)); //注入EF上下文
                        continue;
                    default:
                        throw new Exception("未能找到相应的数据库连接！");
                }


                services.Configure<DbContextOption>(option =>
                {
                    option.IsCodeFirst = dbSetting.IsCodeFirst;
                    option.ModelAssemblyName = dbSetting.ModelAssemblyName;
                    option.ConnectionString = dbSetting.ConnectionString;
                    option.DatabaseType = dbSetting.DatabaseType;
                });
            }

            #endregion

            #region Session

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = "ms";
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
            });

            #endregion

            #region Authentication

            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            services.AddSingleton(policy);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o =>
                {

                    Task RedirectFunc(RedirectContext<CookieAuthenticationOptions> ctx)
                    {
                        ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        return Task.FromResult(0);
                    }

                    o.Cookie.Name = "ngk";
                    o.SlidingExpiration = true;
                    o.ExpireTimeSpan = new TimeSpan(4, 0, 0);

                    o.Events = new CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = RedirectFunc,
                        OnRedirectToAccessDenied = RedirectFunc
                    };
                });

            #endregion

            #region 其他注入

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<PrincipalUser>();
            //services.AddSingleton<IBusinessCacheManager, BusinessCacheManager>();
            services.AddSingleton<ICaptchaClient, TxCaptchaClient>();
            services.AddSingleton<SmsClient, YunPianSmsClient>();


            //注入合约相关实现
            services.AddScoped<ContractClientFactory>()
                .AddScopedAssembly("Contract.Interface", "Contract.Implement.Ngk");
            services.AddSingleton(Configuration) //注入Configuration，ConfigHelper要用
                .AddScopedAssembly("Ngk.DataAccess.Interface", "Ngk.DataAccess.Implement") //注入服务
                .AddScopedAssembly("Ngk.Business.Interface", "Ngk.Business.Implement") //注入服务
                ; //注入服务
            services.AddMvc(
                    options => { options.Filters.Add<ApiAuthorizeFilter>(); })
                .AddControllersAsServices()
                .AddJsonOptions(o =>
                {
                    //                    o.SerializerSettings.Converters.Add(new UnixDateTimeConverter());
                    //                    JsonConvert.DefaultSettings = () => o.SerializerSettings;
                });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            });


            services.AddHttpClient();
            services.AddOptions();
            services.AddAspectCoreContainer();

            #endregion

            return AspectCoreContainer.BuildServiceProvider(services); //接入AspectCore.Injector
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IApplicationLifetime lifetime)
        {
            app.UseCors();
            app.UseSession();
            app.UseGlobalExceptionHandler();

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();
        }
    }
}
