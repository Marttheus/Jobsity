using Jobsity.Application.Events;
using Jobsity.Application.Hubs;
using Jobsity.Application.Interfaces;
using Jobsity.Application.ViewModels;
using MassTransit;
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
        private readonly IRequestClient<StockConsumer> _client;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MessageController(IMessageService messageSerivce, IRequestClient<StockConsumer> client, IServiceScopeFactory serviceScopeFactory)
        {
            _messageSerivce = messageSerivce;
            _client = client;
            _serviceScopeFactory = serviceScopeFactory;
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

            if (newMessageViewModel.Text.Trim().StartsWith("/stock="))
            {
                string stockCode = newMessageViewModel.Text.Trim().Split("=")[1];
                GetStockQuote(stockCode, _serviceScopeFactory, chatHub, _client, newMessageViewModel);
            }

            return CreateResponse(null, "Message successfully sent!", null);
        }

        private async Task GetStockQuote(
            string stockCode,
            IServiceScopeFactory serviceScopeFactory,
            IHubContext<ChatHub> chatHub,
            IRequestClient<StockConsumer> client,
            NewMessageViewModel newMessageViewModel)
        {
            // workaround =)
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var messageService = scope.ServiceProvider.GetService<IMessageService>();
                var response = await client.GetResponse<StockResponse>(new StockConsumer(stockCode));
                await chatHub.Clients.Group(newMessageViewModel.ChatName).SendAsync("ReceiveMessage", "BOT", response.Message.StockQuotePerShare);
                await messageService.Create(new NewMessageViewModel(response.Message.StockQuotePerShare, "BOT", "00000000-0000-0000-0000-000000000000", newMessageViewModel.ChatName, newMessageViewModel.ChatId));
            }
        }
    }
}
