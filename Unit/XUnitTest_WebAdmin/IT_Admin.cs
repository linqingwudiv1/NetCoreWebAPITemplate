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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_factory"></param>
        public IT_Admin(WebApplicationFactory<WebAdminService.Startup> _factory)
        {
            factory = _factory;
            client = this.factory.CreateClient();
        }

        /// <summary>
        /// 集成测试_Role模块
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [Theory]
        [InlineData("api/Roles")]
        public async Task IT_Role(string url) 
        {
            var res = await client.GetAsync(url);
        }
    }
}
