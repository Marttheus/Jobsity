using Jobsity.Presentation.Web.Models;
using Jobsity.Presentation.Web.Models.Account;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobsity.Presentation.Web.Services
{
    public interface IChatService
    {
        User User { get; }
        Task<List<ChatViewModel>> Initialize();
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

        public async Task<List<ChatViewModel>> Initialize()
        {
            var result = await _httpService.Get<Response<List<ChatViewModel>>>("/api/Chat");

            return result.Data;
        }
    }
}