using Jobsity.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Jobsity.Application.Hubs
{
    public class ChatHub : Hub
    {
        protected readonly IChatService _chatService;
        protected readonly IMessageService _messageService;

        public ChatHub(IMessageService messageService, IChatService chatService)
        {
            _messageService = messageService;
            _chatService = chatService;
        }

        public async Task JoinRoom(string group)
        {
            var chat = await _chatService.GetByChatName(group);
            if (chat is null)
                await _chatService.Create(new ViewModels.NewChatViewModel { Name = group });

            await Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public async Task LeaveRoom(string group)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
        }

        public async Task SendMessageToSpecificGroup(string groupName, string sender, string message, Guid chatId, string userId)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", sender, message);
            await _messageService.Create(new ViewModels.NewMessageViewModel { UserId = userId, ChatId = chatId, Text = message });
        }
    }
}
