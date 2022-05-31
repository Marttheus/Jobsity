using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.Infra.CrossCutting.Identity.Models
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public double ExpiresIn { get; set; }
        public UserToken UserToken { get; set; }
    }
}
