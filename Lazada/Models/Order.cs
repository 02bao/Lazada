namespace Lazada.Models
{
    public class Order
    {
        public long Id { get; set; }
        public User? User { get; set; }
        public Shop? Shop { get; set; }
        public List<CartItem>? list_cartitem { get; set; }
        public Status_Order status { get; set; } = Status_Order.cho_thanh_toan;
        public DateTime time { get; set; } = DateTime.UtcNow;
        public long TotalPrice { get; set; }
        //public string CartitemName { get; set; }
        //public long CartiteId { get; set; }
        public Address Address { get; set; }
        //Chi moi tao order cho 1 san pham , tao them order cho nhiu san pham 
        //Tao them lay order bang shopid
        //lay thong tin customer da tung order hang cua shop 
    }

    public enum Status_Order
    {
        cho_thanh_toan,
        dang_xu_ly,
        dang_van_chuyen,
        da_giao,
        da_huy
    }

    public class Order_Get
    {
        public long orderid { get; set; }
        public long userId_order { get; set; }
        public string username_order { get; set; }
        public bool address { get; set; }
        public string CartitemName { get; set; }
        public long TotalPrice { get; set; }
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
