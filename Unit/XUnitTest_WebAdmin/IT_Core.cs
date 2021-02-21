using BaseDLL;
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
    public class IT_Core : IClassFixture<WebApplicationFactory<WebAdminService.Startup>>
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
        readonly IIDGenerator IDGenerator;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_factory"></param>
        public IT_Core(WebApplicationFactory<WebAdminService.Startup> _factory)
        {
            factory = _factory;
            client = this.factory.CreateClient();

            this.IDGenerator = new RedisIDGenerator(new List<string>
            {
                GVariable.configuration["RedisIDGenerator:Passport"]
            },
                GVariable.configuration["RedisIDGenerator:Password"]);

        }


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
