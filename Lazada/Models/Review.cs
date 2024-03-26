namespace Lazada.Models
{
    public class Review
    {
        public long Id { get; set; }
        public User User { get; set; }
        public CartItem CartItem { get; set; }
        public Product Product { get; set; }
        public double rating { get; set; } = 0;
        public string option { get; set; } = "";
        public string? descreption { get; set; } = "";
    }
}
