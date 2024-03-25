namespace Lazada.Models
{
    public class Order
    {
        public long Id { get; set; }
        public User? User { get; set; }
        public Shop? Shop { get; set; }
        public List<string>? list_cart_item { get; set; }
        public Status_Order status { get; set; } = Status_Order.cho_thanh_toan;
        public DateTime time { get; set; } = DateTime.UtcNow;
        public string address { get; set; }
        public string username_order { get; set; }
        public long userId_order { get; set; }
        public string shoprname_order { get; set; }
        public long TotalPrice { get; set; }
        public string CartitemName { get; set; }
        public List<string>? voucher { get; set; }
        public List<Address> Address { get; set; }
    }

    public enum Status_Order
    {
        cho_thanh_toan,
        dang_xu_ly,
        dang_van_chuyen,
        da_giao,
        da_huy
    }
    public class OrderItem
    {
        public long Product_Id { get; set; }
        public string productname { get; set; }
        public long productprice { get; set; }
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
        public string order_status { get; set; }

    }

    public class Order_DTO
    {
        public long address_id { get; set; }
        public List<long> cartitemid { get; set; }
        public List<long > voucherid { get; set; }
    }
}
