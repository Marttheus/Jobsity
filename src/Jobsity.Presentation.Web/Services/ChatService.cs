using Jobsity.Presentation.Web.Models;
using Jobsity.Presentation.Web.Models.Account;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobsity.Presentation.Web.Services
{
    public interface IChatService
    {
        User User { get; }
        Task<List<ChatViewModel>> GetChats();
        Task JoinRoom(string connectionId, string groupName, string groupId);
        Task<List<MessageViewModel>> GetMessagesFromGroup(string groupName);
        Task SendMessage(string message, string sender, string userId, string groupName, string groupId);
        Task CreateRoom(string groupName);
    }

    public class ChatService : IChatService
    {
        private IHttpService _httpService;

        public User User { get; private set; }

        public ChatService(
            IHttpService httpService
        )
        {
            _httpService = httpService;
        }

        public async Task<List<ChatViewModel>> GetChats()
        {
            var result = await _httpService.Get<Response<List<ChatViewModel>>>("/api/Chat");
            return result.Data;
        }

        public async Task JoinRoom(string connectionId, string groupName, string groupId)
        {
            var result = await _httpService.Post<Response>("/api/Chat/Join", new JoinChatViewModel { ConnectionId = connectionId, Name = groupName, Id = groupId });
        }

        public async Task<List<MessageViewModel>> GetMessagesFromGroup(string groupName)
        {
            var result = await _httpService.Get<Response<List<MessageViewModel>>>("/api/Message/" + groupName);

            return result.Data;
        }

        public async Task SendMessage(string message, string sender, string userId, string groupName, string groupId)
        {
            var result = await _httpService.Post<Response>("/api/Message", new NewMessageViewModel(message, sender, userId, groupName, groupId));
        }

        public async Task CreateRoom(string groupName)
        {
            await _httpService.Post<Response>($"/api/Chat/Create/{groupName}");
        }
    }
}