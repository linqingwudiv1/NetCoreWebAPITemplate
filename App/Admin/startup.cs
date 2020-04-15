using Autofac;
using Autofac.Extensions.DependencyInjection;
using DBAccessCoreDLL.Static;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetApplictionServiceDLL;
using Newtonsoft.Json.Serialization;
using NLog;
using System;
using System.Configuration;
using System.IO;
using System.Text;
using WebApp.SingalR;

namespace AdminService
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
        private void InitNLog(IWebHostEnvironment env)
        {
            Logger logger = LogManager.GetLogger("Starup");
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
            }

        }

        /// <summary>
        /// Autofac
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder) 
        {
            //builder.RegisterModule(new CoreModule());
        }

        /// This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Logger log = LogManager.GetLogger("Startup");

            try
            {
                #region Dependency Injection
                // services.AddScoped<ICoreHelper, CoreHelper>((service) =>
                // {
                //     return new CoreHelper();
                // });
                // 
                // services.AddScoped<itestservice, mytestservice>();
                #endregion


                #region Autofac Configration

                services.AddAutofac((builder) =>
                {
                    builder.Populate(services);
                });

                #endregion

                #region Cors Support 跨域支持

                string[] origins = new string[]
{
                    "http://localhost:8080",
                    "http://localhost:8081",
                    "http://localhost:8082",
                    "http://www.wakelu.com",
                    "http://192.168.1.131:8080"
};
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
                string connstr = ConfigurationManager.ConnectionStrings["sqliteTestDB"].ConnectionString;
                
                //services.AddDbContextPool<ExamContextDIP>((opt) =>
                //{
                //    opt.UseSqlite(connstr);
                //}, 100);
                
                #endregion

                #region ApplicationInsights

                services.AddApplicationInsightsTelemetry( opt => 
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
                            ValidateIssuer = true   ,               // 是否验证Issuer
                            ValidateAudience = true ,               // 是否验证Audience
                            ValidateLifetime = true ,               // 是否验证失效时间
                            ClockSkew = TimeSpan.FromSeconds(30),   // 
                            ValidateIssuerSigningKey = true,        // 是否验证SecurityKey
                            ValidAudience = GJWT.Domain ,           // Audience
                            ValidIssuer = GJWT.Domain,              // Issuer,这两项和前面签发jwt的设置一致
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GJWT.SecurityKey)) // 拿到SecurityKey
                        });


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
                  .AddNewtonsoftJson( op => op.SerializerSettings.ContractResolver = new DefaultContractResolver());

                #region Session Config : Redis or Sql Server

                //services.AddDistributedSqlServerCache();

                //services.AddStackExchangeRedisCache( ( opt ) => 
                //{
                //});

                services.AddSession( ( opt ) => 
                {
                    opt.Cookie.HttpOnly     = this.GSessionOpts .Cookie.HttpOnly      ;
                    opt.Cookie.IsEssential  = this.GSessionOpts .Cookie.IsEssential   ;
                    opt.Cookie.Name         = this.GSessionOpts .Cookie.Name          ;
                    opt.Cookie.SecurePolicy = this.GSessionOpts .Cookie.SecurePolicy  ;
                    opt.Cookie.SameSite     = this.GSessionOpts .Cookie.SameSite      ;
                    opt.IdleTimeout         = this.GSessionOpts .IdleTimeout          ;
                });

                #endregion

                #region SingalR

                services.AddSignalR();
                services.AddSingleton<IUserIdProvider, QingUserIdProvider>();

                #endregion

#if DEBUG // Release环境不建议暴露 Swagger 接口
                #region Swagger Doc 文档接入.

                // Register the Swagger generator, defining one or more Swagger documents
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1",
                        new OpenApiInfo
                        {
                            Version = "v1",
                            Title = " WebCoreService Doc",
                            Description = "WebCoreService Doc"
                        }
                    );

                    String basePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, @"SwaggerDoc\");

                    string[] files = Directory.GetFiles(basePath,"*.xml");

                    foreach (var file in files) 
                    {
                        c.IncludeXmlComments(file);
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
        public void Configure( IApplicationBuilder app /*, 
                               IWebHostEnvironment env, 
                               ILoggerFactory loggerFactory */ )
        {

            Logger log = LogManager.GetLogger("Startup");
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

                app.UseAuthentication();

                string path = Path.Combine(Directory.GetCurrentDirectory(), ".Cache");

                app.UseStaticFiles
                (
                    new StaticFileOptions
                    {
                        FileProvider = new PhysicalFileProvider(path),
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
                    c.MapAreaControllerRoute(name: "Exam", areaName: "Exam", pattern: "Exam/{controller=home}/{action=index}");
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
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebCoreService");
                });

                #endregion
            }
            catch (Exception ex)
            {
                log.Info($" Error : { ex.Message } ");
            }
        }
    }
}
