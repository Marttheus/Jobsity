using Jobsity.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.Application.Interfaces
{
    public interface IMessageService
    {
        Task<IList<MessageViewModel>> GetAll();
        Task<MessageViewModel> Create(NewMessageViewModel newMessage);
        Task<IList<MessageViewModel>> GetByChatName(string chatName);
    }
}
