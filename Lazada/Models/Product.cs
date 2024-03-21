namespace Lazada.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
        public int Sold { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
    }

    public class Product_Create
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
    }

    public class Product_update
    {
        public long Id { get; set; }
        public int Sold { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
    }
}
