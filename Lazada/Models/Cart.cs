namespace Lazada.Models
{
    public class Cart
    {
        public long Id { get; set; }
        public Shop Shops { get; set; }
        public User Users { get; set; }
        public long creat_at { get; set; } = DateTime.UtcNow.Ticks / 100;
        public long modified_at { get; set; } = DateTime.UtcNow.Ticks / 100;
        public List<CartItem> CartItems { get; set; }
    }

    public class Cart_see
    {
        public long Id { get; set; }
        public string shop { get; set; } = "";
        public List<CartItem_see> list_cartitem { get; set; }
    }
}
