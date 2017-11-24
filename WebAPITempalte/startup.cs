using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.Configuration.Json;
using Nokia_LTE_WebAPI.Model.EF.Context;
using Microsoft.EntityFrameworkCore;
using Nokia_LTE_WebAPI.Model.Static;

namespace Nokia_LTE_WebAPI
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
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
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        /// This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //跨域支持
            services.AddCors(opt => opt.AddPolicy("WebAPIPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));



            //EF 注入
            /*
            services.AddEntityFrameworkSqlServer().
                AddDbContext<LTEContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
            */


            // Add framework services. 配置项注入
            #region Add framework services. 配置项注入
            services.AddOptions()
                .Configure<Option_ConnctionString>(Configuration.GetSection("ConnectionStrings"));

            services.AddOptions()
                .Configure<Opt_API_LTEUrl>(Configuration.GetSection("APILTEUrl"));
            #endregion

            //services.AddOptions();

            //防止Json序列化-改变对象列的大小写
            services.AddMvc().AddJsonOptions(op => op.SerializerSettings.ContractResolver =
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
                c.IncludeXmlComments(xmlPath);
            });
            #endregion
        }

        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors("WebAPIPolicy");
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseDeveloperExceptionPage();
            app.UseMvc();

            #region Swagger UI

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My_API_V1");
            });

            #endregion

        }
    }
}
