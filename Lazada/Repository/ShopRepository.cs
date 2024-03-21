using Lazada.Data;
using Lazada.Interface;
using Lazada.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Lazada.Repository
{
    public class ShopRepository : IShopRepository
    {
        private readonly LazadaDBContext _context;

        public ShopRepository(LazadaDBContext context)
        {
            _context = context;
        }
        public bool CreateShop(Shop_Create shop, long userId)
        {
            var Emailshop = _context.Shops.SingleOrDefault(s => s.Email == shop.Email);
            if (Emailshop != null)
            {
                return false;
            }

            var user = _context.Users.SingleOrDefault(u => u.Id == userId);
            if(user == null)
            {
                return false;
            }
            var shops = new Shop()
            {
                Name = shop.Name,
                Email = shop.Email,
                Sanpham = shop.Sanpham,
                Phone = "",
                Address = "",
                User = user,
            };
            _context.Shops.Add(shops);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteShop(long id)
        {
            var shopdelete = _context.Shops.SingleOrDefault(s => s.Id == id);
            if(shopdelete == null)
            {
                return false;
            }
            _context.Shops.Remove(shopdelete);
            _context.SaveChanges();
            return true;
        }

        public ICollection<Shop> GetList()
        {
            List<Shop> Shop = _context.Shops.ToList();
            return Shop;
        }

        public List<Shop_User> GetShopByUserId(long userId)
        {
            var shops = _context.Shops.Where(s => s.User.Id == userId).ToList();
            List<Shop_User> shopuser = new List<Shop_User>();
            foreach (var shop in shops)
            {
                shopuser.Add(new Shop_User
                {
                    Id = shop.Id,
                    Name = shop.Name,
                    Email = shop.Email,
                    Sanpham = shop.Sanpham,
                });
            }
            return shopuser;
            
        }

        public Shop GetShopid(long id)
        {
            var shopid = _context.Shops.Where(s => s.Id == id).FirstOrDefault();
            return shopid;
        }

        public bool UpdateShop(Shop_update shopupdate)
        {
            Shop? shops = _context.Shops.Where(s => s.Id == shopupdate.Id).FirstOrDefault();
            if(shops == null)
            {
                return false;
            }
            shops.Phone = shopupdate.Phone;
            shops.Address = shopupdate.Address;
            _context.SaveChanges();
            return true;
        }
    }
}
