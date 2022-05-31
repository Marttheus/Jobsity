using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Jobsity.Application.Hubs
{
    public class ChatHub : Hub
    {
        public async Task JoinRoom(string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public async Task LeaveRoom(string group)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
        }

        public async Task SendMessageToSpecificGroup(string groupName, string sender, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", sender, message);
        }
    }
}
