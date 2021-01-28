using BaseDLL;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTest_WebAdmin
{
    /// <summary>
    /// 集成测试基本
    /// </summary>
    public class IT_Admin : IClassFixture<WebApplicationFactory<WebAdminService.Startup>>
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly WebApplicationFactory<WebAdminService.Startup> factory;
        
        /// <summary>
        /// 
        /// </summary>
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
        /// <returns></returns>
        [Fact]
        public async Task IT_Role(string url) 
        {
            // var res_post = await client.PostAsync
            var res_post = await client.PostAsync("api/Roles");
            Thread.Sleep(200);
            var res_put = await client.PutAsync("api/Roles");
            Thread.Sleep(200);
            var res_get = await client.GetAsync("api/Roles");
            Thread.Sleep(200);
            var res_delete = await client.DeleteAsync("api/Roles");
        }
    }
}
