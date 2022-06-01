namespace Jobsity.Domain.Models
{
    public class Message : Base
    {
        public string Text { get; set; }
        public string UserId { get; set; }
        public Guid ChatId { get; set; }

        public virtual User User { get; set; }
        public virtual Chat Chat { get; set; }
    }
}
