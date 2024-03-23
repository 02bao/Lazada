using Lazada.Models;

namespace Lazada.Interface
{
    public interface IProductRepository
    {
        ICollection<Product> GetList();
        Product GetById(long id);
        bool CreateProduct(Product_Create productcreate, long categoryid, long shopid);
        bool UpdateProduct(Product_update productupdate);
        bool DeleteProduct(long id);
        List<Product_category> GetProductByCategoryId(long categoryid);
        List<Product_Shop> GetProductsByShopId(long shopid);
    }
}
