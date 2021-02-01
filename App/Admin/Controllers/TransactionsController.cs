using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;

namespace WebAdminService.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [EnableCors("WebAPIPolicy")]
    [ApiController]
    public class TransactionsController : BaseController
    {


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Authorize]
        public IActionResult Get() 
        {
            IList<dynamic> list = new List<dynamic>();
            Faker faker = new Faker();

            for (int i = 0; i < 20; i++) 
            {
                list.Add(new 
                {
                    orderId   = Guid.NewGuid(),
                    status    =  faker.Random.ArrayElement(new string[] { "success", "pending" }),
                    timestamp = faker.Date.Past().Date.ToString("yyyy-MM-dd"),
                    username  = faker.Name.FindName(),
                    price     = (faker.Finance.Amount(1000, 15000, 2))
                });
            }
            return JsonToCamelCase(new
            {
                items = list
            }) ;
        }
    }
}