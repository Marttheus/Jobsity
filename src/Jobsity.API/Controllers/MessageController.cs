using Jobsity.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Get(string chatName)
        {
            return CreateResponse(null, null, await _messageSerivce.GetByChatName(chatName));
        }
    }
}
