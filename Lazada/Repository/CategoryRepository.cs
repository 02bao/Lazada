using Lazada.Data;
using Lazada.Interface;
using Lazada.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Lazada.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LazadaDBContext _context;

        public CategoryRepository(LazadaDBContext context)
        {
            _context = context;
        }
        public bool CreateCategory(Categor_Create categorycreate, long shopid)
        {
            var shops = _context.Shops.SingleOrDefault(s => s.Id ==  shopid);
            if(shops == null)
            {
                return false;
            }
            var category = new Category()
            {
                CategoryName = categorycreate.CategoryName,
                ParentCategoryId = categorycreate.ParentCategoryId,
                Description = "",
                Slug = "",
                Shops = shops,
            };
            _context.Categories.Add(category);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteCategory(long id)
        {
            throw new NotImplementedException();
        }

        public Category GetbyId(long id)
        {
            var category = _context.Categories.SingleOrDefault(s => s.Id == id);
            return category;
        }

        public ICollection<Category> GetList()
        {
            List<Category> categories = _context.Categories.ToList();
            return categories;
        }

        public bool UpdateCategory(Category_update categoryUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
