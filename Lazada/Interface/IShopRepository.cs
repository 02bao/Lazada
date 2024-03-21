using Lazada.Models;

namespace Lazada.Interface
{
    public interface IShopRepository
    {
        ICollection<Shop> GetList();
        Shop GetShopid(long id);
        bool CreateShop(Shop_Create shop, long userId);
        bool UpdateShop(Shop_update shopupdate);
        bool DeleteShop(long id);
        List<Shop_User> GetShopByUserId(long userId);
    }
}
