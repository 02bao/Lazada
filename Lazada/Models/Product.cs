namespace Lazada.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public long ProductPrice { get; set; }
        public string Color { get; set; }
        public int Sold { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public Category Category { get; set; }
        public Shop Shop { get; set; }
    }

    public class Product_Create
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public long ProductPrice { get; set; }
        public string Color { get; set; }
    }

    public class Product_update
    {
        public long Id { get; set; }
        public int Sold { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
    }

    public class Product_category
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public long ProductPrice { get; set; } = 0;
        public string Color { get; set; }
        public int Sold { get; set; }
        public string Brand { get; set; }
    }

    public class Product_Shop
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public long ProductPrice { get; set; }
        public string Brand { get; set; }
    }
}
