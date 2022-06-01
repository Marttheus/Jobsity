using Jobsity.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.Application.Interfaces
{
    public interface IChatService
    {
        Task<IList<ChatViewModel>> GetAll();
        Task<ChatViewModel> Create(NewChatViewModel newChat);
        Task<ChatViewModel> GetByChatName(string chatName);
    }
}
