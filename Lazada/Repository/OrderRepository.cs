using Lazada.Data;
using Lazada.Interface;
using Lazada.Models;
using Microsoft.EntityFrameworkCore;
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
        public bool AddtoOrder(long userId, long cartitemId )
        {
            var users = _context.Users.SingleOrDefault(s => s.Id == userId);
            if(users == null)
            {
                return false;
            }
            var cartitem = _context.CartItems.Include(ci => ci.Carts)
                                              .ThenInclude(Carts => Carts.Shops)
                                              .SingleOrDefault(ci => ci.Id == cartitemId);
            if(cartitem == null)
            {
                return false;
            }
            Shop shops = _context.Shops.Where(s => s.Id == cartitem.Carts.Shops.Id).FirstOrDefault();
            var order = new Order();
            var cartItemDetail = $"{users.Name}, {cartitem.Carts.Shops.Name}";
            order.User = users;
            order.Shop = shops;
            order.list_cart_item = new List<string>
            {
                cartItemDetail
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
            return true;
        }

        public List<Order_User> GetOrderByUserId(long userId)
        {
            List<Order_User>  response = new List<Order_User>();
            User? user = _context.Users.SingleOrDefault(s => s.Id == userId);
            if(user == null) 
            {
                return response;
            }
            List<Order> orders = _context.Orders.Where( s => s.User == user)
                                                .Include(p => p.Shop).ToList();
            foreach(var order in orders)
            {
                Order_User myorder = new Order_User();
                myorder.orderid = order.Id;
                myorder.shopname = order.Shop.Name;
                myorder.shopid = order.Shop.Id;
                List<OrderItem> orderItems = new List<OrderItem>();

                List<CartItem> cartitems = _context.CartItems.Where(ci => ci.Carts.Id == order.Id).ToList();
                foreach(CartItem item in cartitems)
                {
                    OrderItem orderitem = new OrderItem();
                    orderitem.Product_Id = item.Product.Id;
                    orderitem.productname = item.Product.ProductName;
                    orderitem.productprice = item.Product.ProductPrice;
                    orderitem.quantity = item.quantity;
                    orderitem.option = item.option;
                    orderItems.Add(orderitem);
                }
                myorder.list_orderitem = orderItems;
                
                response.Insert(0, myorder);
            }
            return response;
        }
        
    }
}
