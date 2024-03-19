namespace Lazada.Models
{
    public class User
    {
        public long Id { get; set; } = DateTime.UtcNow.Ticks;
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class User_login
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
