using Jobsity.Domain.Models;

namespace Jobsity.Domain.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<IList<Message>> GetMessagesFromChat(string chatName);
    }
}
