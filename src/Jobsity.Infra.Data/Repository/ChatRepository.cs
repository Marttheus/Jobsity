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
    public class ChatRepository : Repository<Chat>, IChatRepository
    {
        public ChatRepository(DataContext context) : base(context)
        {
            
        }

        public async Task<Chat?> GetByChatName(string chatName)
        {
            return await _dataContext.Set<Chat>().Where(x => x.Name == chatName).FirstOrDefaultAsync();
        }
    }
}
