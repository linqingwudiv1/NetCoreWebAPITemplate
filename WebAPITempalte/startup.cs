﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using DBAccessDLL.Static;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using NLog;
using System;
using System.IO;
using WebAPI.AutofacModule;
using WebApp.SingalR;

namespace WebAPI
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {

        /// <summary>
        /// 
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        private SessionOptions GSessionOpts;

        /// <summary>
        /// 
        /// </summary>
        private void InitNet4Log()
        {
            //net4log = LogManager.CreateRepository("NETCoreRepository");
            //XmlConfigurator.Configure(net4log, new FileInfo(@"\.Config\net4log.config"));
        }
        private void InitNLog() 
        {
            Logger logger = LogManager.GetLogger("test");
            logger.Trace("测试test");
            logger.Info("测试test");
            logger.Warn("测试test");
            logger.Error("测试test");
            logger.Fatal("测试test");
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitSessionOpts()
        {
            #region Session Option
            GSessionOpts = new SessionOptions();

            GSessionOpts.Cookie.HttpOnly = true;
            GSessionOpts.Cookie.IsEssential = true;
            GSessionOpts.Cookie.Name = ".AspNetCore.Session";
            GSessionOpts.Cookie.SecurePolicy = CookieSecurePolicy.None;
            GSessionOpts.Cookie.SameSite = SameSiteMode.Lax;
            GSessionOpts.IdleTimeout = TimeSpan.FromMinutes(1440);
            #endregion 
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitOther()
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\.Cache\ExportExcel"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\.Cache\ExportExcel");
            }

            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\.Cache\Image"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\.Cache\Image");
            }

            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\.LocalDB"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\.LocalDB");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        public Startup(IWebHostEnvironment env)
        {
            #region Initilize

            InitNet4Log();
            InitNLog();
            InitSessionOpts();
            InitOther();

            #endregion

            IConfigurationBuilder builder = new ConfigurationBuilder()
                                                .AddInMemoryCollection()
                                                .SetBasePath(env.ContentRootPath)
                                                .AddJsonFile(@".Config\appsettings.json", optional: false, reloadOnChange: true)
                                                .AddJsonFile($@".Config\appsettings.{env.EnvironmentName}.json", optional: true)
                                                .AddJsonFile(@".Config\ConnectionString.json", optional: false, reloadOnChange: true)
                                                .AddJsonFile(@".Config\APILTEUrl.json", optional: false, reloadOnChange: true)
                                                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        /// <summary>
        /// Autofac 注入
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacExamModule());
        }

        /// This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Logger log = LogManager.GetLogger("Startup", typeof(Startup));

            try
            {
                string[] origins = new string[]
                {
                    "http://localhost:8080",
                    "http://localhost:8081",
                    "http://localhost:8082",
                    "http://wakelu.com",
                    "http://192.168.1.131:8080"
                };

                // Cors Support 跨域支持


                services.AddCors(opt => opt.AddPolicy("WebAPIPolicy", builder =>
                {
                    builder.WithOrigins(origins)
                           .AllowAnyMethod()
                           .AllowCredentials()
                           .AllowAnyHeader();
                }));

                #region Add framework services. 配置项注入

                #region EF DI注入

                /*
                services.AddEntityFrameworkSqlServer().
                    AddDbContext<LTEContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
                */

                //services.AddEntityFrameworkSqlServer().AddDbContext<ExamContext>(opt => opt);

                #endregion

                #region Autofac

                services.AddAutofac( c=> 
                {
                });

                #endregion

                #region ApplicationInsights

                services.AddApplicationInsightsTelemetry( opt => 
                {

                });

                #endregion

                services.AddOptions()
                        .Configure<Option_ConnctionString>(Configuration.GetSection("ConnectionStrings"))
                        .Configure<Opt_API_LTEUrl>(Configuration.GetSection("APILTEUrl"));

                #endregion

                //防止Json序列化-改变对象列的大小写

                services.AddControllersWithViews(opts => 
                {
                    opts.EnableEndpointRouting = false;
                }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                  .AddNewtonsoftJson(op => op.SerializerSettings.ContractResolver = new DefaultContractResolver()); 

                services.AddRazorPages(opts => 
                {
                }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                  .AddNewtonsoftJson( op => op.SerializerSettings.ContractResolver = new DefaultContractResolver());

                #region Session Config : Redis or Sql Server

                //services.AddStackExchangeRedisCache(opts =>
                //{
                //    opts.Configuration = "localhost";
                //    opts.InstanceName = "SampleInstance";
                //    opts.ConfigurationOptions.Password = "abc123,";
                //});

                services.AddSession( ( opt ) => 
                {
                    opt.Cookie.HttpOnly     = this.GSessionOpts .Cookie.HttpOnly      ;
                    opt.Cookie.IsEssential  = this.GSessionOpts .Cookie.IsEssential   ;
                    opt.Cookie.Name         = this.GSessionOpts .Cookie.Name          ;
                    opt.Cookie.SecurePolicy = this.GSessionOpts .Cookie.SecurePolicy  ;
                    opt.Cookie.SameSite     = this.GSessionOpts .Cookie.SameSite      ;
                    opt.IdleTimeout         = this.GSessionOpts.IdleTimeout           ;
                });


                #endregion


                #region SingalR

                services.AddSignalR();

                services.AddSingleton<IUserIdProvider, QingUserIdProvider>();

                #endregion

#if DEBUG
                #region Swagger Doc 文档接入. 生产环境不暴露 Swagger
                // Register the Swagger generator, defining one or more Swagger documents
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1",
                        new OpenApiInfo
                        {
                            Version = "v1",
                            Title = " WebAPI 文档",
                            Description = "WebAPI 文档"
                            //TermsOfService = "www.cnblogs.com/linqing"
                        }
                    );

                    // Set the comments path for the Swagger JSON and UI.
                    String basePath = PlatformServices.Default.Application.ApplicationBasePath;
                    String xmlPath = Path.Combine(basePath, @"Doc\Swagger\WebAPIDoc.xml");

                    if (File.Exists(xmlPath))
                    {
                        c.IncludeXmlComments(xmlPath);
                    }
                    else
                    {
                        log.Error($"[Error]: Swagger :No Exists Path : " + xmlPath);
                    }
                });

                #endregion
#endif
            }
            catch (Exception ex)
            {
                log.Info($"error :{ex.Message}");
            }

        }

        /// <summary>
        ///  This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure( IApplicationBuilder app, 
                               IWebHostEnvironment env, 
                               ILoggerFactory loggerFactory )
        {
            //ILog log = LogManager.GetLogger(net4log.Name, typeof(Startup));
            Logger log = LogManager.GetLogger("Startup", typeof(Startup));
            try
            {
                #region MVC 和WebAPI 相关

                app.UseRouting();

                #region 其他常用配置
                app.UseHsts();
                app.UseHttpsRedirection();
                app.UseCookiePolicy();
                app.UseCors("WebAPIPolicy");
                app.UseSession(this.GSessionOpts);
                app.UseAuthentication();
                app.UseAuthorization();

                string path = Path.Combine(Directory.GetCurrentDirectory(), ".Cache");
                
                app.UseStaticFiles
                ( new StaticFileOptions
                  {
                      FileProvider = new PhysicalFileProvider(path),
                      RequestPath = "/Cache"
                  });

                #endregion

                app.UseEndpoints(c =>
                {
                    //MVC
                    c.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                    // WebAPI
                    c.MapControllerRoute("WebAPI", "api/{controller=Test}/{action=HelloNetCore}/{id?}");
                    // Area
                    c.MapAreaControllerRoute(name: "TestArea", areaName: "TestArea", pattern: "TestArea/{controller}/{action}");
                });

                #endregion



                #region SingalR
                app.UseEndpoints(c =>
                {
                    c.MapHub<CommonHub>("/commonHub");
                });
                #endregion

                #region Swagger

                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My_API_V1");
                });

                #endregion
            }
            catch (Exception ex)
            {
                log.Info($"error :{ex.Message}");
            }
        }
    }
}
