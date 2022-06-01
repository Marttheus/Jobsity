using AutoMapper;
using Jobsity.Application.ViewModels;
using Jobsity.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Message, MessageViewModel>()
                .ConstructUsing(x => new MessageViewModel { Text = x.Text, Group = x.Chat.Name, Sender = x.User.UserName, CreatedAt = x.CreatedAt });
            CreateMap<Chat, ChatViewModel>()
                .ConstructUsing(x => new ChatViewModel { Id = x.Id, Name = x.Name });
        }
    }
}
