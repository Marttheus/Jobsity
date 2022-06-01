using Jobsity.Domain.Models;

namespace Jobsity.Domain.Interfaces
{
    public interface IChatRepository : IRepository<Chat>
    {
        Task<Chat?> GetByChatName(string chatName);
    }
}
