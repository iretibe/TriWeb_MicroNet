namespace MicroNet.Identity.Models
{
    public class ActiveSession
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserId { get; set; } = default!;
        public string SessionId { get; set; } = default!;
        public DateTime LoginTime { get; set; }
        public DateTime LastActivityTime { get; set; }
    }
}
