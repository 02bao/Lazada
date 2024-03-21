namespace Lazada.Models
{
    public class Shop
    {
        public long Id { get; set; } = DateTime.UtcNow.Ticks / 100;
        public string Name { get; set; }
        public string Email { get; set; }
        public string Sanpham { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public User User { get; set; }
        public List<Category> Category { get; set; }
    }

    public class Shop_Create
    {
        public long Id { get; set; } = DateTime.UtcNow.Ticks / 100;
        public string Name { get; set; }
        public string Email { get; set; }
        public string Sanpham { get; set; }
    }

    public class Shop_update
    {
        public long Id { get; set; } = DateTime.UtcNow.Ticks / 100;
        public string Phone { get; set; }
        public string Address { get; set; }
    }

    public class Shop_User
    {
        public long Id { get; set; } = DateTime.UtcNow.Ticks / 100;
        public string Name { get; set; }
        public string Email { get; set; }
        public string Sanpham { get; set; }
    }
}
