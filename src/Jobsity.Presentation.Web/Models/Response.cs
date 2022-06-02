using System;
using System.Collections.Generic;

namespace Jobsity.Presentation.Web.Models
{
    public class Response<T>
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }

    public class Response
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; }
        public object Data { get; set; }
    }

    public class SignInResponse
    {
        public string StatusMessage { get; set; }
        public AuthResponse? AuthResponse { get; set; }
    }

    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public double ExpiresIn { get; set; }
        public UserToken UserToken { get; set; }
    }

    public class UserToken
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }

    public class ChatViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class JoinChatViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ConnectionId { get; set; }
    }
    public class MessageViewModel
    {
        public string Text { get; set; }
        public string Sender { get; set; }
        public string Group { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class NewMessageViewModel
    {
        public string Text { get; set; }
        public string Sender { get; set; }
        public string UserId { get; set; }
        public string ChatName { get; set; }
        public string ChatId { get; set; }

        public NewMessageViewModel(string text, string sender, string userId, string chatName, string chatId)
        {
            Text = text;
            Sender = sender;
            UserId = userId;
            ChatName = chatName;
            ChatId = chatId;
        }
    }
}
