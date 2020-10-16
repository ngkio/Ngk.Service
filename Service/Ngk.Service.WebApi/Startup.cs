using AspectCore.Extensions.DependencyInjection;
using Contract.Interface;
using log4net;
using log4net.Repository;
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
using Ngk.Service.WebApi.Common;
using System;
using System.IO;
using Swashbuckle.AspNetCore.Swagger;
using Thor.Framework.Common.Helper;
using Thor.Framework.Common.IoC;
using Thor.Framework.Common.Options;
using Thor.Framework.Data.DbContext.Mongo;
using Thor.Framework.Data.DbContext.Relational;
using Thor.Framework.Data.Model;
using Thor.Framework.Service.WebApi.Middleware;

namespace Ngk.Service.WebApi
{
    public class Startup
    {
        private static ILoggerRepository Repository { get; set; }

        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
            Environment = env;
            //初始化log4net
            Repository = LogManager.CreateRepository("NGKRepository");
            Log4NetHelper.SetConfig(Repository, $"log4net.{env.EnvironmentName}.config");
        }

        private IConfiguration Configuration { get; }
        private IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

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

            //var policy = new AuthorizationPolicyBuilder()
            //    .RequireAuthenticatedUser()
            //    .Build();
            //services.AddSingleton(policy);

            //// IdentityServer
            //services.AddAuthentication(Configuration["IdentityService:DefaultScheme"])
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = Configuration["IdentityService:Uri"];
            //        options.JwtValidationClockSkew = TimeSpan.FromSeconds(0);
            //        options.RequireHttpsMetadata = Convert.ToBoolean(Configuration["IdentityService:UseHttps"]);
            //    });

            #endregion

            #region 其他注入

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<PrincipalUser>();
            services.AddScoped<YunPianSmsClient>();
            //添加 YunPian 服务
            services.AddYunPianService(options =>
                options.ApiKey = Configuration["YunPianApiKey"]
            );
            services.AddSingleton<ICaptchaClient, TxCaptchaClient>();
            services.AddSingleton<IdentityServiceClient>();

            //注入合约相关实现
            services.AddScoped<ContractClientFactory>()
                .AddScopedAssembly("Contract.Interface", "Contract.Implement.Ngk");
            services.AddSingleton(Configuration) //注入Configuration，ConfigHelper要用
                .AddScopedAssembly("Ngk.DataAccess.Interface", "Ngk.DataAccess.Implement") //注入服务
                .AddScopedAssembly("Ngk.Business.Interface", "Ngk.Business.Implement") //注入服务
                ; //注入服务

            services
                .AddMvc(options =>
                {
                    //options.Filters.Add<ApiAuthorizeFilter>();
                })
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("ngk",
                    new Info
                    {
                        Title = "Ngk Service API",
                        Version = "v1.0",
                        Description =
                            "进行数据推送和修改的都采用POST，获取数据的都采用GET。POST推送时，通过Body传输Json数据；GET参数请通过url传值。所有返回都将采用Json数据格式。若为多语言请求，请在请求头里增加lang传值，目前包含语言头：zh、zh_tw、en、ja、ar、ko、fr、es、ru、pt、de"
                    });

                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                var xmlPath = Path.Combine(basePath, "Ngk.Service.xml");
                c.IncludeXmlComments(xmlPath, true);
                xmlPath = Path.Combine(basePath, "Ngk.DataAccess.DTO.xml");
                c.IncludeXmlComments(xmlPath, true);
            });

            services.AddHttpClient();
            services.AddOptions();
            services.AddAspectCoreContainer();
            services.AddConsulService();

            #endregion

            return AspectCoreContainer.BuildServiceProvider(services); //接入AspectCore.Injector
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IApplicationLifetime lifetime)
        {
            app.UseCors();
            app.UseGlobalExceptionHandler();

            //app.UseAuthentication();

            app.UseMvcWithDefaultRoute();

            app.UseSwagger(c => { c.RouteTemplate = "/doc/{documentName}/swagger.json"; });
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/core/doc/ngk/swagger.json", "Ngk Service API V1.0"); });

            if (Environment.IsProduction())
                app.RegisterConsul(lifetime, Configuration);
        }
    }
}