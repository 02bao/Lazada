namespace Lazada.Models
{
    public class Order
    {
        public long Id { get; set; }
        public User User { get; set; }
        public Shop Shop { get; set; }
        public long Shopid { get; set; }
        public List<string> list_cart_item { get; set; }
    }
}
