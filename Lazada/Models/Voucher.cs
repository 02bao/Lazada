namespace Lazada.Models
{
    public class Voucher
    {
        public long Id { get; set; }
        public string title { get; set; } = "";
        public DateTime public_date { get; set; } = DateTime.UtcNow.ToUniversalTime();
        public DateTime expire_date { get; set; } = DateTime.UtcNow.ToUniversalTime();
        public int discount { get; set; } = 0;
        public long? product_voucher { get; set; }
        public List<string>? list_user_applied { get; set; }
        public Shop Shop { get; set; }
        public User? User { get; set; }
    }

    public class Voucher_Add
    {
        public string title { get; set; } = "";
        public DateTime public_date { get; set; } = DateTime.UtcNow.ToUniversalTime();
        public DateTime expire_date { get; set; } = DateTime.UtcNow.ToUniversalTime();
        public int discount { get; set; } = 0;
    }

    public class Voucher_Product
    {
        public long voucherId { get; set; }
        public string title { get; set; }
        public int discount { get; set; }
    }
}
