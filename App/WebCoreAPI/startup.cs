using Autofac;
using BaseDLL;
using BusinessCoreDLL.AutofacModule;
using DBAccessBaseDLL.Static;
using DBAccessCoreDLL.EFORM.Context;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.PlatformAbstractions;
//using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetApplictionServiceDLL;
using NetApplictionServiceDLL.Swagger;
using Newtonsoft.Json.Serialization;
using NLog;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using WebApp.SingalR;

/// <summary>
/// 
/// </summary>
namespace WebCoreService
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

        #region private method

        /// <summary>
        /// 
        /// </summary>
        private void InitNet4Log()
        {
            //net4log = LogManager.CreateRepository("NETCoreRepository");
            //XmlConfigurator.Configure(net4log, new FileInfo(@"\.Config\net4log.config"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        private void InitNLog(IWebHostEnvironment env)
        {
            Logger logger = LogManager.GetLogger("Startup");
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
        /// Initilize Directory
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



        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        public Startup(IWebHostEnvironment env)
        {
            #region Initilize

            InitNet4Log();
            InitNLog(env);
            InitSessionOpts();
            InitOther();

            #endregion

            if (env != null)
            {
                IConfigurationBuilder builder = new ConfigurationBuilder()
                                    .AddInMemoryCollection()
                                    .SetBasePath(env.ContentRootPath)
                                    .AddJsonFile(@".Config\appsettings.json", optional: false, reloadOnChange: true)
                                    .AddJsonFile($@".Config\appsettings.{env.EnvironmentName}.json", optional: true)
                                    .AddJsonFile(@".Config\ConnectionString.json", optional: false, reloadOnChange: true)
                                    .AddJsonFile(@".Config\APILTEUrl.json", optional: false, reloadOnChange: true)
                                    .AddEnvironmentVariables();

                Configuration = builder.Build();
                GVariable.configuration = this.Configuration;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
           builder.RegisterModule(new CoreModule());
        }

        /// This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Logger log = LogManager.GetLogger("Startup");

            try
            {
                #region Dependency Injection Case
                // services.AddScoped<ICoreHelper, CoreHelper>((service) =>
                // {
                //     return new CoreHelper();
                // });
                // 
                // services.AddScoped<itestservice, mytestservice>();
                #endregion

                #region Workflow-Core

                services.AddWorkflow();

                #endregion

                #region Autofac Configration

                //services.AddAutofac((builder) =>
                //{
                //    Console.WriteLine("---------------- autofac lambda...");
                //    builder.Populate(services);
                //});

                #endregion

                #region Cors Support 跨域支持
                string[] origins = ConfigurationManager.AppSettings.Get("CorsSite")?.Split(",");

                if ( origins == null || 
                     origins.Length <= 0 )
                {
                    origins = new string[]
                                    {
                    "http://localhost:8080",
                    "http://localhost:8081",
                    "http://localhost:8082",
                    "http://192.168.1.131:8080",
                    "https://localhost:44311"
                                    };
                }
                Console.WriteLine("=============cors surpport  start=============");
                Console.WriteLine(origins);
                Console.WriteLine("=============cors surpport  end=============");
                services.AddCors(opt => opt.AddPolicy("WebAPIPolicy", builder =>
                {
                    builder.WithOrigins(origins)
                           .AllowAnyMethod()
                           .AllowCredentials()
                           .AllowAnyHeader();
                }));

                #endregion

                #region Add framework services. 配置项注入

                #region EF DI注入
                // string connstr = ("PostgreSQLCoreDB");
                services.AddDbContextPool<CoreContextDIP>((opt) =>
                {
                    // opt.UseNpgsql(connstr);
                    // opt.UseSqlite(connstr);
                }, 100);

                #endregion

                #region ApplicationInsights

                services.AddApplicationInsightsTelemetry(opt =>
                {
                });

                #endregion

                services.AddOptions()
                        .Configure<Option_ConnctionString>(Configuration.GetSection("ConnectionStrings"))
                        .Configure<Opt_API_LTEUrl>(Configuration.GetSection("APILTEUrl"));

                // services.AddOptions()

                #endregion

                #region Jwt

                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(opt =>
                        opt.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,               // 是否验证Issuer
                            ValidateAudience = true,               // 是否验证Audience
                            ValidateLifetime = true,               // 是否验证失效时间
                            ClockSkew = TimeSpan.FromSeconds(30),   // 
                            ValidateIssuerSigningKey = true,        // 是否验证SecurityKey
                            ValidAudience = GJWT.Domain,           // Audience
                            ValidIssuer = GJWT.Domain,              // Issuer,这两项和前面签发jwt的设置一致
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GJWT.SecurityKey)) // 拿到SecurityKey
                        });


                #endregion

                #region MassTransit

                services.AddMassTransit(MassTransitConfig.MTConfiguration);
                services.AddMassTransitHostedService();

                #endregion

                //防止Json序列化-改变对象列的大小写

                services.AddControllersWithViews(opts =>
                {
                    opts.EnableEndpointRouting = false;
                }).SetCompatibilityVersion(CompatibilityVersion.Latest)
                  .AddNewtonsoftJson(op => op.SerializerSettings.ContractResolver = new DefaultContractResolver());

                services.AddRazorPages(opts =>
                {
                }).SetCompatibilityVersion(CompatibilityVersion.Latest)
                  .AddNewtonsoftJson(op => op.SerializerSettings.ContractResolver = new DefaultContractResolver());

                #region Session Config : Redis or Sql Server

                //services.AddDistributedSqlServerCache();

                //services.AddStackExchangeRedisCache( ( opt ) => 
                //{
                //});

                services.AddSession((opt) =>
                {
                    opt.Cookie.HttpOnly = this.GSessionOpts.Cookie.HttpOnly;
                    opt.Cookie.IsEssential = this.GSessionOpts.Cookie.IsEssential;
                    opt.Cookie.Name = this.GSessionOpts.Cookie.Name;
                    opt.Cookie.SecurePolicy = this.GSessionOpts.Cookie.SecurePolicy;
                    opt.Cookie.SameSite = this.GSessionOpts.Cookie.SameSite;
                    opt.IdleTimeout = this.GSessionOpts.IdleTimeout;
                });

                #endregion

                #region SingalR

                services.AddSignalR();
                services.AddSingleton<IUserIdProvider, QUserIdProvider>();

                #endregion

#if DEBUG // Release 环境不建议暴露 Swagger 接口
                #region Swagger Doc 文档接入.

                // Register the Swagger generator, defining one or more Swagger documents

                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1",
                        new OpenApiInfo
                        {
                            Version = "v1",
                            Title = $" Web Core Service Doc",
                            Description = $"Web Core Service Doc",
                        }
                    );

                    c.SchemaFilter<ExampleValueSchemaFilter>();

                    String basePath = Path.GetFullPath(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, @"SwaggerDoc\"));
                    String[] files = Directory.GetFiles(basePath, "*.xml");

                    foreach (var file in files)
                    {
                        c.IncludeXmlComments(file);
                    }


                    #region JWT 验证配置

                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme. \n\r (Exam Header: Authorization:Bearer {yourJwtToken}   ) "
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                              new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new string[] {}
                        }
                    });

                    #endregion
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
        public void Configure(IApplicationBuilder app /*, 
                               IWebHostEnvironment env, 
                               ILoggerFactory loggerFactory */ )
        {

            Logger log = LogManager.GetLogger("Startup");
            try
            {
                #region 确保数据库生成，并暖机

                string dbconn = GConnStrings.PostgreSQLCoreDBConn;

                DbContextOptions<CoreContextDIP> opt = new DbContextOptions<CoreContextDIP>();
                using (var db = new CoreContextDIP(opt))
                {
#if DEBUG
                    db.Database.EnsureCreated();

#endif
                    db.Accounts.FirstOrDefaultAsync();
                    db.Roles.FirstOrDefaultAsync();
                    db.RoutePages.FirstOrDefaultAsync();
                    db.RoutePageRoles.FirstOrDefaultAsync();
                    db.AccountRoles.FirstOrDefaultAsync();

                    //db.WarmUp();
                }

                #endregion

                #region MVC 和WebAPI 相关

                app.UseRouting();

                #region 其他常用配置

                app.UseHsts();
                app.UseHttpsRedirection();
                app.UseCookiePolicy();
                app.UseCors(GWebVariable.CQRSPolicy);
                app.UseSession(this.GSessionOpts);
                app.UseAuthentication();
                app.UseAuthorization();
                app.UseMvc();

                #region static files
                {
                    FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();
                    // add mime type case  
                    provider.Mappings.Add(".fbx", "application/octet-stream");
                    provider.Mappings.Add(".obj", "application/octet-stream");
                    app.UseStaticFiles(new StaticFileOptions
                    {
                        
                        ContentTypeProvider = provider
                    });
                }
                #endregion


                app.UseAuthentication();

                string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                string cachePath = Path.Combine(Directory.GetCurrentDirectory(), ".Cache");

                //app.UseStaticFiles(new StaticFileOptions 
                //{
                //    FileProvider = new PhysicalFileProvider(rootPath),
                //    RequestPath = "/public"
                //});
                app.UseStaticFiles
                (
                    new StaticFileOptions
                    {
                        FileProvider = new PhysicalFileProvider(cachePath),
                        RequestPath = "/Cache"
                    }
                );

                #endregion

                app.UseEndpoints(c =>
                {
                    //MVC
                    c.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                    // WebAPI
                    c.MapControllerRoute("WebAPI", "api/{controller=Test}/{action=HelloNetCore}/{id?}");
                    // Area
                    c.MapAreaControllerRoute(name: "Exam", areaName: "Exam", pattern: "Exam/{controller}/{action}");
                    c.MapAreaControllerRoute(name: "AppAPI", areaName: "AppAPI", pattern: "AppAPI/{controller}/{action}");

                });

                app.UseEndpoints(c =>
                {
                    c.MapControllerRoute("default", "{controller=Home}/{action=Index}");
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
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core_API_V1");
                });

                #endregion


            }
            catch (Exception ex)
            {
                log.Info($" Error : { ex.Message } ");
                Console.WriteLine($" Error : { ex.Message } ");
                Debug.WriteLine($" Error : { ex.Message } ");
            }
        }
    }
}
