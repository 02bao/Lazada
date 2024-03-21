using Lazada.Data;
using Lazada.Interface;
using Lazada.Models;

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
            throw new NotImplementedException();
        }

        public ICollection<Category> GetList()
        {
            throw new NotImplementedException();
        }

        public bool UpdateCategory(Category_update categoryUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
