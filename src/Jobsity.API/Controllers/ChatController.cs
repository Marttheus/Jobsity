using Jobsity.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Get()
        {
            return CreateResponse(null, null, await _chatService.GetAll());
        }
    }
}
