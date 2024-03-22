namespace Lazada.Models
{
    public class Order
    {
        public long Id { get; set; }
        public User User { get; set; }
        public Shop Shop { get; set; }
        public List<string> list_cart_item { get; set; }
    }

    public class OrderItem
    {
        public long Product_Id { get; set; }
        public string productname { get; set; }
        public string productprice { get; set; }
        public long cartitem_id { get; set; }
        public string? option { get; set; }
        public int quantity { get; set; }
    }

    public class Order_User
    {
        public long orderid { get; set; }
        public string shopname { get; set; } = "";
        public long shopid { get; set; }
        public List<OrderItem> list_orderitem { get; set; } = new List<OrderItem>();

    }
}
