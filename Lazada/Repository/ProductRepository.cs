using Lazada.Data;
using Lazada.Interface;
using Lazada.Models;

namespace Lazada.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly LazadaDBContext _context;

        public ProductRepository(LazadaDBContext context)
        {
            _context = context;
        }

        public bool CreateProduct(Product_Create productcreate, long categoryid)
        {
            var category = _context.Categories.SingleOrDefault(s => s.Id == categoryid);
            if (category == null)
            {
                return false;
            }
            var product = new Product()
            {
                ProductName = productcreate.ProductName,
                ProductPrice = productcreate.ProductPrice,
                Color = productcreate.Color,
                Sold = 0,
                Description="",
                Brand="",
                Category = category,
            };
            _context.Products.Add(product);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteProduct(long id)
        {
            throw new NotImplementedException();
        }

        public Product GetById(long id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Product> GetList()
        {
            throw new NotImplementedException();
        }

        public List<Product_category> GetProductByCategoryId(long categoryid)
        {
            throw new NotImplementedException();
        }

        public bool UpdateProduct(Product_update productupdate)
        {
            throw new NotImplementedException();
        }
    }
}
