using Jobsity.Application.Hubs;
using Jobsity.Application.Interfaces;
using Jobsity.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Jobsity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ApiControllerBase
    {
        private readonly IMessageService _messageSerivce;

        public MessageController(IMessageService messageSerivce)
        {
            _messageSerivce = messageSerivce;
        }

        [HttpGet("{chatName}")]
        [Authorize]
        public async Task<IActionResult> GetMessagesFromChat(string chatName)
        {
            return CreateResponse(null, null, await _messageSerivce.GetByChatName(chatName));
        }

        [HttpPost]
        [Authorize] 
        public async Task<IActionResult> SendMessage(
            [FromBody] NewMessageViewModel newMessageViewModel,
            [FromServices] IHubContext<ChatHub> chatHub)
        {
            await chatHub.Clients.Group(newMessageViewModel.ChatName).SendAsync("ReceiveMessage", newMessageViewModel.Sender, newMessageViewModel.Text);
            await _messageSerivce.Create(newMessageViewModel);

            return CreateResponse(null, "Message successfully sent!", null);
        }
    }
}
