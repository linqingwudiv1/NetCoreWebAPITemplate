using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace WebApp.SingalR
{
    /// <summary>
    /// 通用监听事件.
    /// </summary>
    public class CommonHub : Hub
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }


        /// <summary>
        /// 通知二维码页面
        /// </summary>
        /// <param name="UE4ChildKey"></param>
        /// <param name="ImgPath"></param>
        /// <returns></returns>
        public async Task UploadImageComplated(string UE4ChildKey, string ImgPath)
        {
            IClientProxy ChildUE4 = Clients.Clients(UE4ChildKey);

            if (ChildUE4 != null)
            {
                await ChildUE4.SendAsync("ReceiveUploadImageComplated", ImgPath);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {

            return base.OnConnectedAsync();
        }
    }
}
