namespace Lazada.Models
{
    public class Address
    {
        public long Id { get; set; }
        public User Users { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Address_Detail { get; set; }
        public bool Address_Default { get; set; } = false;
        public List<Order> Order { get; set; }
    }

    public class Address_User
    {
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Address_Detail { get; set; }
    }

    public class Address_Short
    {
        public long Address_Id { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public bool Address_Default { get; set; }
        public string Address_Full { get; set; }
    }

}
