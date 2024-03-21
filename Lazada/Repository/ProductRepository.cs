using Lazada.Data;
using Lazada.Interface;
using Lazada.Models;
using Microsoft.AspNetCore.Http.HttpResults;

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
            var products = _context.Products.SingleOrDefault(s => s.Id == id);
            if(products == null)
            {
                return false;
            }
            _context.Products.Remove(products);
            _context.SaveChanges();
            return true;
        }

        public Product GetById(long id)
        {
            var product = _context.Products.SingleOrDefault(s => s.Id == id);
            return product;
        }

        public ICollection<Product> GetList()
        {
            List<Product> products = _context.Products.ToList();
            return products;
        }

        public List<Product_category> GetProductByCategoryId(long categoryid)
        {
            var products = _context.Products.Where(s => s.Category.Id == categoryid);
            List<Product_category> productcategory = new List<Product_category>();
            foreach(var  product in products)
            {
                productcategory.Add(new Product_category
                {
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    Sold = product.Sold,
                    Color = product.Color,
                    Brand = product.Brand,
                });
            }
            return productcategory;
        }

        public bool UpdateProduct(Product_update productupdate)
        {
            var products = _context.Products.SingleOrDefault(s => s.Id ==  productupdate.Id);
            if(products == null)
            {
                return false;
            }
            products.Sold = productupdate.Sold;
            products.Description = productupdate.Description;
            products.Brand = productupdate.Brand;
            _context.SaveChanges();
            return true;
        }
    }
}
