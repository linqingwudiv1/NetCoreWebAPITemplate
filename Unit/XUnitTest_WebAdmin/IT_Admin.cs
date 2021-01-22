using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTest_WebAdmin
{
    /// <summary>
    /// 集成测试基本
    /// </summary>
    public class IT_Admin : IClassFixture<WebApplicationFactory<WebAdminService.Startup>>
    {
        protected readonly WebApplicationFactory<WebAdminService.Startup> factory;
        protected HttpClient client;

        public IT_Admin(WebApplicationFactory<WebAdminService.Startup> _factory)
        {
            //var setting = new ConnectionStringSettings("PostgreSQLCoreDB", "Username=postgres;Password=1qaz@WSX;Host=192.168.1.172;Port=5432;Database=QCoreDB;");
            //ConfigurationManager.ConnectionStrings.Add(setting);
            factory = _factory;
            client = this.factory.CreateClient();

            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            // You may want to map to your own exe.config file here.
            fileMap.ExeConfigFilename = @"D:\Work_LQ\template\NetCoreWebAPITemplate\Unit\XUnitTest_WebAdmin\App.config";
            // You can add here LocalUserConfigFilename, MachineConfigFilename and RoamingUserConfigFilename, too
            System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

        }



        [Theory]
        [InlineData("/")]
        public async Task IT_Role(string url) 
        {
            
            var res =await client.GetAsync(url);
        }
    }
}
