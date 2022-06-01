using Jobsity.Presentation.Web.Models;
using Jobsity.Presentation.Web.Models.Account;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobsity.Presentation.Web.Services
{
    public interface IMessageService
    {
        Task<List<MessageViewModel>> GetMessagesFromGroup(string groupName);
    }

    public class MessageService : IMessageService
    {
        private IHttpService _httpService;

        public User User { get; private set; }

        public MessageService(
            IHttpService httpService
        ) {
            _httpService = httpService;
        }

        public async Task<List<MessageViewModel>> GetMessagesFromGroup(string groupName)
        {
            var result = await _httpService.Get<Response<List<MessageViewModel>>>("/api/Message/" + groupName);

            return result.Data;
        }
    }
}