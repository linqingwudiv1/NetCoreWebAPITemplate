using Autofac;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Nokia_LTE_WebAPI.Model.Static;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using WebAPITempalte.AutofacModule;

namespace Nokia_LTE_WebAPI
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        public static ILoggerRepository net4log { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("ConnectionString.json", optional: false, reloadOnChange: true)
                .AddJsonFile("APILTEUrl.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            net4log = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(net4log, new FileInfo("net4log.config"));

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
            var log = LogManager.GetLogger(net4log.Name, typeof(Startup));
            try
            {
                //跨域支持
                services.AddCors(opt => opt.AddPolicy("WebAPIPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                }));

                #region Add framework services. 配置项注入

                #region EF注入
                /*
                services.AddEntityFrameworkSqlServer().
                    AddDbContext<LTEContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
                */
                #endregion

                services.AddOptions()
                    .Configure<Option_ConnctionString>(Configuration.GetSection("ConnectionStrings"));

                services.AddOptions()
                    .Configure<Opt_API_LTEUrl>(Configuration.GetSection("APILTEUrl"));

                #endregion

                //防止Json序列化-改变对象列的大小写
                services.AddMvc()
                        .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                        .AddJsonOptions(op => op.SerializerSettings.ContractResolver =
                          new Newtonsoft.Json.Serialization.DefaultContractResolver());

                #region Swagger 文档接入
                // Register the Swagger generator, defining one or more Swagger documents
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1",
                        new Info
                        {
                            Version = "v1",
                            Title = " API 文档",
                            Description = "API 文档",
                            TermsOfService = "l.q"
                        }
                    );

                    // Set the comments path for the Swagger JSON and UI.
                    var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                    var xmlPath = Path.Combine(basePath, "WebAPITempalte.xml");

                    if (File.Exists(xmlPath))
                    {
                        c.IncludeXmlComments(xmlPath);
                    }
                    else
                    {
                        log.Info($"Swagger :No Exists Path : " + xmlPath);
                    }
                });
                #endregion


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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var log = LogManager.GetLogger(net4log.Name, typeof(Startup));
            try
            {
                app.UseCors("WebAPIPolicy");
                
                app.UseDeveloperExceptionPage();

                #region MVC 和WebAPI 相关
                app.UseMvc(routes =>
                {
                    routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");// MVC
                    routes.MapRoute("WebAPI", "api/{controller=Test}/{action=HelloNetCore}/{id?}");//WebAPI
                    routes.MapAreaRoute(name: "TestArea_route", areaName:"TestArea",template: "TestArea/{controller=TestArea}/{action=Index}/{id?}");  //Area
                });
                #endregion


                #region Swagger UI

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
