using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalLearn.Hubs
{
    public class ChatHub : Hub
    {

        //定义服务端 "SendMessage" 处理方法,用于处理 Web客户端"SendMessage"事件。
        public async Task SendMessage(string user, string message)
        {
            //重点关注ReceiveMessage 事件，后面参数为： 发送人，消息内容
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        /// <summary>
        /// 直接回复请求的Client消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessageToCaller(string message)
        {          
            await Clients.Caller.SendAsync("SendMessageToCaller", "From Server Reply:" + DateTime.Now.ToString("yyyy-MM-dd") + "  " + message);
        }

        /// <summary>
        /// 分组消息发送
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task SendMessageToGroups(string message)
        {          
            List<string> groups = new List<string>() { "SignalR Users" };
            return Clients.Groups(groups).SendAsync("SendMessageToGroups", message);
        }

        /// <summary>
        /// 客户端连接后，默认添加到 SignalR Users 分组
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
