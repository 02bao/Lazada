namespace Lazada.Models
{
    public class Notification
    {
        public long Id { get; set; }
        public User? User { get; set; }
        public Shop? Shop { get; set; }
        public string title { get; set; }
        public DateTime time_sent { get; set; } = DateTime.UtcNow;
        public string descrepstion { get; set; }
        public bool isSeen { get; set; }
        public DateTime time_seen { get; set; } = DateTime.UtcNow.AddDays(-1);
        public bool isDelete { get; set; } = false;
        public bool type_receive { get; set; } //true la buyer, false la cua shop 
    }

    public class NotiResponse
    {
        public long Id { get; set; }
        public string title { get; set; }
        public DateTime time_sent { get; set; } = DateTime.UtcNow;
        public string descrepstion { get; set; }
        public bool isSeen { get; set; }
        public DateTime time_seen { get; set; } = DateTime.UtcNow.AddDays(-1);
    }
}
