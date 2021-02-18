using DBAccessBaseDLL.IDGenerator;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTest_WebAdmin
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitTest_Base : IClassFixture<WebApplicationFactory<WebAdminService.Startup>>
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
        public UnitTest_Base(WebApplicationFactory<WebAdminService.Startup> _factory, IIDGenerator _IDGenerator)
        {
            factory = _factory;
            client = this.factory.CreateClient();
        }

        readonly IIDGenerator IDGenerator;

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void RedisGeneratorPerformanceTest() 
        {
            for (int i = 0; i < 100; i++) 
            {
                Task.Run( () => 
                {
                    for (int x = 0; x < 100000; x++) 
                    {
                        IDGenerator.GetNewID<PerformanceTest>();
                    }
                });

            }

        }
    }

    public class PerformanceTest 
    {
    }
}
