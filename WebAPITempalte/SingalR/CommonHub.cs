using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.SingalR
{
    /// <summary>
    /// 通用监听事件.
    /// </summary>
    public class CommonHub : Hub
    {
        public static readonly Dictionary<string, string> Dict = new Dictionary<string, string>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(string user, string message)
        {
            await Clients.User(user).SendAsync("ReceiveMessage", user, "hello world!");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override  Task OnConnectedAsync()
        {
            string identity = this.Context.UserIdentifier;

            this.Clients.User(this.Context.ConnectionId).SendAsync("ReceiveOnConnected", this.Context.ConnectionId);

            return base.OnConnectedAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            
            return base.OnDisconnectedAsync(exception);
        }
    }
}
