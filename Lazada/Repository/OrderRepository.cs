using Lazada.Data;
using Lazada.Interface;
using Lazada.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace Lazada.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly LazadaDBContext _context;

        public OrderRepository(LazadaDBContext context)
        {
            _context = context;
        }
        public bool AddtoOrder(Order order, long cartitemId)
        {
            Order newOrder = new Order();
            //kiểm tra mặt hàng có trong giỏi hàng không.
            var cartItem = _context.CartItems.Include( c => c.Carts).ThenInclude( u => u.Users)
                                             .Include( p => p.Product).ThenInclude(s => s.Shop) 
                                             .SingleOrDefault(ci => ci.Id == cartitemId);
            if(cartItem == null)
            {
                return false;
            }
            //Tạo mới một đơn hàng 
            var NewOrder = new Order
            {
                User = cartItem.Carts.Users,
                Shop = cartItem.Product.Shop,
                list_cart_item = new List<CartItem>()
            };
            //Lặp qua các mặt hàng trong giỏi hàng và xem chúng có cùng 1 cửa hàng hay không
            var cartItemSameShop = cartItem.Carts.CartItems
                                            .Where(ci => ci.Product.Shop.Id == NewOrder.Shop.Id)
                                            .ToList();

            //Thêm các mặt hàng vào đơn hàng 
            foreach(var item in cartItemSameShop )
            {
                newOrder.list_cart_item.Add(item);
            }
            _context.Orders.Add(newOrder);
            _context.SaveChanges();
            return true;

        }
    }
}
