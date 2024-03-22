namespace Lazada.Models
{
    public class Order
    {
        public long Id { get; set; }
        public User User { get; set; }
        public Shop Shop { get; set; }
        public List<CartItem> list_cart_item { get; set; }
    }
}
