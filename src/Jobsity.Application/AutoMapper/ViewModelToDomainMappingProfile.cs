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
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<NewChatViewModel, Chat>()
                .ConstructUsing(x => new Chat { Name = x.Name });

            CreateMap<NewMessageViewModel, Message>()
                .ConstructUsing(x => new Message { Text = x.Text, UserId = x.UserId, ChatId = x.ChatId });
        }
    }
}
