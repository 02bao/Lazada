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
        public bool AddtoOrder( long cartitemId, long userId )
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
            var order = new Order();
            
            var cartItemDetail = $"{users.Name}, {cartitem.Carts.Shops.Name}";
            order.User = users;
            order.Shopid = cartitem.Carts.Shops.Id;
            order.list_cart_item = new List<string>
            {
                cartItemDetail
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
            return true;
        }

        
    }
}
