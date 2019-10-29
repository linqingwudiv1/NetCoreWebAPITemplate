using Microsoft.AspNetCore.SignalR;

/// <summary>
/// 
/// </summary>
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

            if (connection == null) 
            {
                return "";
            }

            //One connection One ID 
            return connection.ConnectionId;
        }
    }
}
