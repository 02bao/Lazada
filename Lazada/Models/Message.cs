namespace Lazada.Models
{
    public class Message
    {
        public long Id { get; set; }
        public bool type { get; set; } // true la nguoi nhan , false la shop nhan
        public string message { get; set; }
        public DateTime time { get; set; } = DateTime.UtcNow;
        public Conversation Conversation { get; set; }
    }

    public class Message_US
    {
        public bool type { get; set; }
        public DateTime time { get; set; }
        public string message { get; set; }
    }
}
