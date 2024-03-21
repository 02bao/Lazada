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
            var category = _context.Categories.SingleOrDefault(s => s.Id == id);
            if(category == null)
            {
                return false;
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return true;
        }

        public Category GetbyId(long id)
        {
            var category = _context.Categories.SingleOrDefault(s => s.Id == id);
            return category;
        }

        public List<Category_shop> GetCategoryByshopid(long shopid)
        {
            var categories = _context.Categories.Where(s => s.Shops.Id == shopid);
            List<Category_shop> categoryshop = new List<Category_shop>();
            foreach(var category in categories)
            {
                categoryshop.Add(new Category_shop
                {
                    CategoryName = category.CategoryName,
                    ParentCategoryId = category.ParentCategoryId,
                    Description = category.Description,
                });
            }
            return categoryshop;
        }

        public ICollection<Category> GetList()
        {
            List<Category> categories = _context.Categories.ToList();
            return categories;
        }

        public bool UpdateCategory(Category_update categoryUpdate)
        {
            var categories = _context.Categories.SingleOrDefault(s => s.Id == categoryUpdate.Id);
            if(categories == null)
            {
                return false;
            }
            categories.Description = categoryUpdate.Description;
            categories.Slug = categoryUpdate.Slug;
            _context.SaveChanges();
            return true;
        }
    }
}
