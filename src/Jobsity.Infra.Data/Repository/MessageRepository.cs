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
    }
}
