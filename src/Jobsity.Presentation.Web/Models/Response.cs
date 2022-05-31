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
        public Guid Id { get; set; }
        public string Email { get; set; }
    }
}
