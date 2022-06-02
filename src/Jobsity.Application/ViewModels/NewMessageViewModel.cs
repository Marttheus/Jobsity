using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.Application.ViewModels
{
    public class NewMessageViewModel
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public string Sender { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string ChatName { get; set; }
        [Required]
        public Guid ChatId { get; set; }

        public NewMessageViewModel(string text, string sender, string userId, string chatName, Guid chatId)
        {
            Text = text;
            Sender = sender;
            UserId = userId;
            ChatName = chatName;
            ChatId = chatId;
        }
    }
}
