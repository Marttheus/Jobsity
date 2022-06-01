using Jobsity.Domain.Interfaces;
using Jobsity.Domain.Models;
using Jobsity.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.Infra.Data.Repository
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(DataContext context) : base(context)
        {
            
        }

        public async Task<IList<Message>> GetMessagesFromChat(string chatName)
        {
            var chat = await _dataContext.Set<Chat>().FirstAsync(x => x.Name == chatName);

            return await _dataContext.Set<Message>()
                .Include(x => x.User)
                .Include(x => x.Chat)
                .Where(x => x.ChatId == chat.Id)
                .ToListAsync();
        }

        public async Task<Message> Create(Message obj)
        {
            await _dataContext.AddAsync(obj);
            await _dataContext.SaveChangesAsync();

            return await _dataContext.Set<Message>()
                .Include(x => x.User)
                .Include(x => x.Chat)
                .FirstAsync(x => x.Id == obj.Id);
        }
    }
}
