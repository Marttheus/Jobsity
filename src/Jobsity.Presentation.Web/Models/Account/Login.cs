using System.ComponentModel.DataAnnotations;

namespace Jobsity.Presentation.Web.Models.Account
{
    public class Login
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}