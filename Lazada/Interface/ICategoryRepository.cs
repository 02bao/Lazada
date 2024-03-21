using Lazada.Models;

namespace Lazada.Interface
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetList();
        Category GetbyId(long id);
        bool CreateCategory(Categor_Create categorycreate, long shopid);
        bool UpdateCategory(Category_update categoryUpdate);
        bool DeleteCategory(long id);
    }
}
