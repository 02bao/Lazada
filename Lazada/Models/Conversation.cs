namespace Lazada.Models
{
    public class Conversation
    {
        public long Id { get; set; }
        public User User { get; set; }
        public Shop Shop { get; set; }
        public bool is_seen { get; set; }
        public DateTime lasttime { get; set; }
        public List<Message> Message { get; set; }
    }
    public class Conversation_US
    {
        public string username { get; set; }
        public string shopname { get; set; }
        public List<Message_US>? messages { get; set; }
    }

}
