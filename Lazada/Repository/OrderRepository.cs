using Lazada.Data;
using Lazada.Interface;
using Lazada.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Web.Helpers;


namespace Lazada.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly LazadaDBContext _context;

        public OrderRepository(LazadaDBContext context)
        {
            _context = context;
        }

        public bool AddtoOrder(List<long> cartitem_id, long addressid, List<long> voucherid)
        {
            Order newOrder = new Order();
            newOrder.voucher = new List<string>();
            List<CartItem> list_cartitem = _context.CartItems.Include(s => s.Product)
                          .Where(s => cartitem_id.Contains(s.Id)).Include(s => s.Carts)
                          .ThenInclude(s => s.Shops).ToList();
            if (!list_cartitem.Any())
            {
                return false;
            }
            Address address = _context.Addresses.Include(s => s.Users).SingleOrDefault(s => s.Id == addressid);
            if(address == null)
            {
                return false;
            }
            List<Voucher> list_voucher = _context.Vouchers.Where(s => voucherid.Contains(s.Id)).ToList();
            if (list_voucher.Any())
            {
                foreach(Voucher item in list_voucher)
                {
                    item.list_user_applied.Add(address.Users.Id.ToString());
                    string data = JsonConvert.SerializeObject(item);
                    newOrder.voucher.Add(data);
                }
            }
            User user = address.Users;
            List<CartItem> tmp = list_cartitem;
            while(tmp.Where(s => s.Status == Status_cart_item.active).Count() > 0)
            {
                Shop shop = _context.Shops.Where(s => s.Id == tmp[0].Carts.Shops.Id).FirstOrDefault();
                newOrder.User = user;
                newOrder.Shop = shop;
                newOrder.address = JsonConvert.SerializeObject(address, Formatting.Indented, 
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    });
                List<string> seri_cartitem = new List<string>();
                for(int i = 0; i< tmp.Count; i++)   
                {
                    if (tmp[i].Carts.Shops.Id == shop.Id)
                    {
                        list_cartitem[i].Status = Status_cart_item.order;
                        list_cartitem[i].order = newOrder;
                        string data = JsonConvert.SerializeObject(list_cartitem[i], Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                            });
                        seri_cartitem.Add(data);
                    }
                }
                newOrder.list_cart_item = seri_cartitem;
                _context.Orders.Add(newOrder);
            }
            _context.SaveChanges();
            return true;
        }

        

        //public List<Order_User> GetOrderByUserId(long userId)
        //{
        //    List<Order_User>  response = new List<Order_User>();
        //    User? user = _context.Users.SingleOrDefault(s => s.Id == userId);
        //    if(user == null) 
        //    {
        //        return response;
        //    }
        //    List<Order> orders = _context.Orders.Where( s => s.User == user)
        //                                        .Include(p => p.Shop).ToList();
        //    foreach(Order order in orders)
        //    {
        //        Order_User myorder = new Order_User();

        //        List<CartItem> cartitems = new List<CartItem>();
        //        foreach(string cartitem in order.list_cart_item)
        //        {
        //            //CartItem tmp = JsonConvert.DeserializeObject<CartItem>(cartitem);
        //            //cartitems.Add(tmp);
        //        }
        //        List<OrderItem> orderItems = new List<OrderItem>();
        //        foreach(CartItem item in cartitems)
        //        {
        //            OrderItem orderitem = new OrderItem();
        //            orderitem.Product_Id = item.Product.Id;
        //            orderitem.productname = item.Product.ProductName;
        //            orderitem.productprice = item.Product.ProductPrice;
        //            orderitem.quantity = item.quantity;
        //            orderitem.option = item.option;
        //            orderitem.cartitem_id = item.Id;
        //            orderItems.Add(orderitem);
        //        }
        //        myorder.list_orderitem = orderItems;
        //        myorder.orderid = order.Id;
        //        myorder.shopname = order.Shop.Name;
        //        myorder.shopid = order.Shop.Id;
        //        response.Insert(0, myorder);
        //    }
        //    return response;
        //}

    }
}
