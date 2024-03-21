namespace Lazada.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string CategoryName { get; set; }
        public int ParentCategoryId { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public Shop Shops { get; set; }
    }
    public class Categor_Create
    {
        public long Id { get; set; }
        public string CategoryName { get; set; }
        public int ParentCategoryId { get; set; }
        
    }
    public class Category_update
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
    }
}
