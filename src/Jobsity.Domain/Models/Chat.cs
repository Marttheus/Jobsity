namespace Jobsity.Domain.Models
{
    public class Chat : Base
    {
        public string Name { get; set; }

        public virtual List<User> Users { get; set; }
        public virtual List<Message> Messages { get; set; }
    }
}
