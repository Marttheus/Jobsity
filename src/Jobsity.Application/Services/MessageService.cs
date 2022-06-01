﻿using AutoMapper;
using Jobsity.Application.Interfaces;
using Jobsity.Application.ViewModels;
using Jobsity.Domain.Interfaces;
using Jobsity.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.Application.Services
{
    public class MessageService : IMessageService
    {
        protected readonly IMessageRepository _messageRepository;
        protected readonly IMapper _mapper;

        public MessageService(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<MessageViewModel> Create(NewMessageViewModel newMessage)
        {
            var message = _mapper.Map<Message>(newMessage);
            message = await _messageRepository.Create(message);
            return _mapper.Map<MessageViewModel>(message);
        }

        public async Task<IList<MessageViewModel>> GetAll()
        {
            return _mapper.Map<IList<MessageViewModel>>(await _messageRepository.GetAll());
        }

        public async Task<IList<MessageViewModel>> GetByChatName(string chatName)
        {
            return _mapper.Map<IList<MessageViewModel>>(await _messageRepository.GetMessagesFromChat(chatName));
        }
    }
}
