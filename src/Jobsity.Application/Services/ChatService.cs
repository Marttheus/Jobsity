using AutoMapper;
using Jobsity.Application.Interfaces;
using Jobsity.Application.ViewModels;
using Jobsity.Domain.Interfaces;
using Jobsity.Domain.Models;

namespace Jobsity.Application.Services
{
    public class ChatService : IChatService
    {
        protected IChatRepository _chatRepository;
        protected IMapper _mapper;

        public ChatService(IChatRepository chatRepository, IMapper mapper)
        {
            _chatRepository = chatRepository;
            _mapper = mapper;
        }

        public async Task<ChatViewModel> Create(NewChatViewModel newChat)
        {
            var chat = _mapper.Map<Chat>(newChat);
            chat = await _chatRepository.Add(chat);
            return _mapper.Map<ChatViewModel>(chat);
        }

        public async Task<IList<ChatViewModel>> GetAll()
        {
            return _mapper.Map<IList<ChatViewModel>>(await _chatRepository.GetAll());
        }

        public async Task<ChatViewModel> GetByChatName(string chatName)
        {
            return _mapper.Map<ChatViewModel>(await _chatRepository.FindFirst(x => x.Name == chatName));
        }
    }
}
