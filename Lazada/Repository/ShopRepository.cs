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
