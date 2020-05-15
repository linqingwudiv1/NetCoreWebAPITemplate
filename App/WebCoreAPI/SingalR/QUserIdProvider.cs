using Microsoft.AspNetCore.SignalR;

namespace WebApp.SingalR
{
    /// <summary>
    /// 
    /// </summary>
    public class QUserIdProvider : IUserIdProvider
    {
        /// <summary>
        /// Provider user identity of Connection 
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public string GetUserId(HubConnectionContext connection)
        {
            if (connection == null) 
            {
                return "";
            }

            //One connection One ID 
            return connection.ConnectionId;
        }
    }
}
