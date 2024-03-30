using Lazada.Data;
using Lazada.Interface;
using Lazada.Models;
using Microsoft.AspNetCore.Mvc;
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

        public bool AddtoOrder(long userid, long cartitemids, long voucherid)
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

            //Them truong hop ko co id voucher thi nhap ko va xu ly 
            //quan ly tgian han su dung voucher
            var voucher_discount = 0;
            long Pricediscount = 0;
            var cartItem = _context.CartItems.Include(s => s.Product)
                                                      .Include(s => s.Carts)
                                                      .ThenInclude(s => s.Shops)
                                                      .Where(s => s.Id == cartitemids &&
                                                        s.Carts.Users.Id == userid && 
                                                        s.Status == Status_cart_item.active)
                                                      .FirstOrDefault();
            if (cartItem != null)
            {
                if (voucherid != 0)
                {
                    var Voucherapplied = _context.Vouchers.Include(s => s.User)
                                                           .Where(s => s.Id == voucherid &&
                                                           s.User.Id == userid &&
                                                           s.expire_date > DateTime.Now)
                                                           .FirstOrDefault();
                    if (Voucherapplied != null)
                    {
                        voucher_discount = Voucherapplied.discount;
                    }
                }
                if (cartItem.Product.inventory >= cartItem.quantity)
                {
                    Pricediscount = cartItem.Product.ProductPrice - cartItem.Product.ProductPrice * (voucher_discount / 100);
                    cartItem.Status = Status_cart_item.order;
                }
                else
                {
                    return false;
                }
            }
            Shop shop = _context.Shops.SingleOrDefault(s => s.Id == cartItem.Carts.Shops.Id);
            if (shop == null)
                {
                    cartItem.Status = Status_cart_item.active;
                    _context.SaveChanges();
                    return false;
                }
                Order newOrder = new Order
                {
                    User = user,
                    Shop = shop,
                    Address = addressdefault,
                    TotalPrice = Pricediscount,
                    time = DateTime.UtcNow,
                    status = Status_Order.cho_thanh_toan,
                    list_cartitem = new List<CartItem> { cartItem},
                };
                _context.Orders.Add(newOrder);
                _context.SaveChanges();
                return true;
        }

        public List<Order_Get> GetOrderbyUserId(long userId)
        {
            List<Order_Get> response = new List<Order_Get>();
            User user = _context.Users.SingleOrDefault(s => s.Id == userId);
            if (user == null)
            {
                return response;
            }
            Address address = _context.Addresses.Include(s => s.Order).SingleOrDefault(s => s.Users.Id == userId);
            var orders = _context.Orders.Where(s => s.User.Id == userId)
                .Select(s => new Order_Get
                {
                    orderid = s.Id,
                    userId_order = user.Id,
                    username_order = user.Name,
                    address = address.Address_Default,
                    CartitemName = s.CartitemName,
                    TotalPrice = s.TotalPrice
                }).ToList();
            return orders;
        }

        public bool CancleOrder(long orderId)
        {
            Order orders = _context.Orders.SingleOrDefault(s => s.Id == orderId);
            if (orders == null)
            {
                return false;
            }
            _context.Orders.Remove(orders);
            _context.SaveChanges();
            return true;
        }
    }
}
