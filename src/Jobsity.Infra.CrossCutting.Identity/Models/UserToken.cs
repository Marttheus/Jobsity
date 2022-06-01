using NetDevPack.Identity.Jwt.Model;

namespace Jobsity.Infra.CrossCutting.Identity.Models
{
    public class UserToken
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }
}
