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
        /// <summary>
        /// Event
        /// </summary>
        public static readonly string Event_ReceiveMessage = "ReceiveMessage";
        /// <summary>
        /// Event
        /// </summary>
        public static readonly string Event_ReceiveOnConnected = "ReceiveOnConnected";
        /// <summary>
        /// Event
        /// </summary>
        public static readonly string Event_ReceiveUploadImageComplated = "ReceiveUploadImageComplated";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(string user, string message)
        {
            await Clients.User(user).SendAsync(Event_ReceiveMessage, user, "hello world!");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override  Task OnConnectedAsync()
        {
            string identity = this.Context.UserIdentifier;
            this.Clients.User(this.Context.ConnectionId).SendAsync(Event_ReceiveOnConnected, identity);

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
