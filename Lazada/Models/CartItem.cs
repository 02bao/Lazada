﻿namespace Lazada.Models
{
    public class CartItem
    {
        public long Id { get; set; }
        public Cart Carts { get; set; }
        public Product Product { get; set; }
        public string option { get; set; }
        public int quantity { get; set; } = 1;
        public Order? order { get; set; }

    }
    public class CartItem_add
    {
        public long Id { get; set; }
        public string option { get; set; }
        public int quantity { get; set; } = 1;
    }
    
    public class CartItem_see
    {
        public long Id { get; set; }
        public long Product_Id { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
        public string Color { get; set; }
        public int quantity { get; set; } = 1;
    }
}
