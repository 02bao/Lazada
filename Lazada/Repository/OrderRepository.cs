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

        public bool AddtoOrder(long userid, long cartitemid, long voucherid)
        {
            User user = _context.Users.SingleOrDefault(s => s.Id == userid);
            if (user == null)
            {
                return false;
            }
            Address addressdefault = _context.Addresses.Where(s => s.Users.Id == userid
            && s.Address_Default).FirstOrDefault();
            if (addressdefault == null)
            {
                return false;
            }
            var voucher_discount = 0;
            long Pricediscount = 0;
            Voucher Voucherapplied = _context.Vouchers.Include(s => s.User).Where(s => s.Id == voucherid
                                        && s.User.Id == userid).FirstOrDefault();
            if (Voucherapplied != null)
            {
                voucher_discount = Voucherapplied.discount;
            }
            CartItem cartItem = _context.CartItems.Include(s => s.Product).Include(s => s.Carts).ThenInclude(s => s.Shops)
                .Where(s => s.Id == cartitemid &&
               s.Carts.Users.Id == userid && s.Status == Status_cart_item.active).FirstOrDefault();
            if (cartItem != null)
            {
                Pricediscount = cartItem.Product.ProductPrice - cartItem.Product.ProductPrice * (voucher_discount / 100);
                cartItem.Status = Status_cart_item.order;
            }
            Shop shop = _context.Shops.SingleOrDefault(s => s.Id == cartItem.Carts.Shops.Id);
            if (shop == null)
            {
                return false;
            }
            Order newOrder = new Order
            {
                userId_order = user.Id,
                username_order = user.Name,
                shoprname_order = shop.Name,
                address = addressdefault.Address_Detail,
                TotalPrice = Pricediscount,
                CartitemName = cartItem.Product.ProductName,
            };
            _context.Orders.Add(newOrder);
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
