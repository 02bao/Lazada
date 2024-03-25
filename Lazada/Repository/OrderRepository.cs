﻿using Lazada.Data;
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

        public bool CancleOrder(long orderId)
        {
            Order orders = _context.Orders.SingleOrDefault(s => s.Id == orderId);
            if(orders == null)
            {
                return false;
            }
            _context.Orders.Remove(orders);
            _context.SaveChanges();
            return true;
        }

        public List<Order_Get> GetOrderbyUserId(long userId)
        {
            List<Order_Get> response = new List<Order_Get>();
            User user = _context.Users.SingleOrDefault(s => s.Id == userId);
            if(user == null)
            {
                return response;
            }
            var oders = _context.Orders.Where(s => s.userId_order == userId)
                .Select(s => new Order_Get
                {
                    orderid = s.Id,
                    userId_order = s.userId_order,
                    username_order = user.Name,
                    address = s.address,
                    shoprname_order = s.shoprname_order,
                    CartitemName = s.CartitemName,
                    TotalPrice = s.TotalPrice
                }).ToList();
            return oders;
        }





       
    }
}
