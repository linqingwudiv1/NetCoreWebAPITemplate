using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using NetApplictionServiceDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.SingalR
{
    public class QingUserIdProvider : IUserIdProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public string GetUserId(HubConnectionContext connection)
        {
            return "ID_" + connection.GetHttpContext().Session.GetString("username");
        }
    }
}
