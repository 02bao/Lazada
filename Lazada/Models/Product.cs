namespace Lazada.Models
{
    public class Product
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public long ProductPrice { get; set; }
        public string Color { get; set; }
        public int Sold { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public Category Category { get; set; }
        public Shop Shop { get; set; }
        public int inventory { get; set; }
        public List<Voucher>? Voucher { get; set; }//shop quan ly so luong san pham trong kho
    }


    public class Product_Create
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public long ProductPrice { get; set; }
        public string Color { get; set; }
        public int inventory { get; set; }

    }

    public class Product_update
    {
        public long ProductId { get; set; }
        public int Sold { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public int inventory { get; set; }
    }

    public class Product_category
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public long ProductPrice { get; set; } = 0;
        public string Color { get; set; }
        public int Sold { get; set; }
        public string Brand { get; set; }
        public int inventory { get; set; }

    }

    public class Product_Shop
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public long ProductPrice { get; set; }
        public string Brand { get; set; }
        public int inventory { get; set; }

    }
}
