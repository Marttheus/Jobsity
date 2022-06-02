using Jobsity.Application.Hubs;
using Jobsity.Application.Interfaces;
using Jobsity.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Jobsity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ApiControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetChats()
        {
            return CreateResponse(null, null, await _chatService.GetAll());
        }

        [HttpPost("Create/{chatName}")]
        [Authorize]
        public async Task<IActionResult> CreateChat(
            string chatName,
            [FromServices] IHubContext<ChatHub> chatHub)
        {
            var message = "A chat with that name already exists.";

            var chat = await _chatService.GetByChatName(chatName);

            if (chat is null)
            {
                message = "Chat successfully created!";
                chat = await _chatService.Create(new NewChatViewModel(chatName));
                await chatHub.Clients.All.SendAsync("CreateChat", chat.Id, chat.Name);
            }

            return CreateResponse(null, message, chat);
        }

        [HttpPost("Join")]
        [Authorize]
        public async Task<IActionResult> JoinChat(
            [FromBody] JoinChatViewModel joinChatViewModel,
            [FromServices] IHubContext<ChatHub> chatHub)
        {
            await chatHub.Groups.AddToGroupAsync(joinChatViewModel.ConnectionId, joinChatViewModel.Name);

            return CreateResponse(null, "User successfully joined the chat!", null);
        }
    }
}
