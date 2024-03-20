using Lazada.Data;
using Lazada.Interface;
using Lazada.Models;

namespace Lazada.Repository
{
    public class ShopRepository : IShopRepository
    {
        private readonly LazadaDBContext _context;

        public ShopRepository(LazadaDBContext context)
        {
            _context = context;
        }
        public bool CreateShop(Shop_Create shop)
        {
            var Emailshop = _context.Shops.SingleOrDefault(s => s.Email == shop.Email);
            if (Emailshop != null)
            {
                return false;
            }
            var shops = new Shop()
            {
                Name = shop.Name,
                Email = shop.Email,
                Sanpham = shop.Sanpham,
                Phone ="",
                Address=""
            };
            _context.Shops.Add(shops);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteShop(long id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Shop> GetList()
        {
             List<Shop> Shop = _context.Shops.ToList();
            return Shop;
        }

        public bool GetShopid(long id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateShop(Shop shop)
        {
            throw new NotImplementedException();
        }
    }
}
