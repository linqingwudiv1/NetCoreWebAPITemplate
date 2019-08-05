using Microsoft.AspNetCore.SignalR;

namespace WebApp.SingalR
{
    /// <summary>
    /// 
    /// </summary>
    public class QingUserIdProvider : IUserIdProvider
    {
        /// <summary>
        /// Provider user identity of Connection 
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public string GetUserId(HubConnectionContext connection)
        {
            //One Conn One ID 
            return connection.ConnectionId;
        }
    }
}
