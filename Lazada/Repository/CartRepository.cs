using Lazada.Data;
using Lazada.Interface;
using Lazada.Models;
using Microsoft.EntityFrameworkCore;

namespace Lazada.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly LazadaDBContext _context;

        public CartRepository(LazadaDBContext context)
        {
            _context = context;
        }
        public bool AddtoCart(long userId, CartItem_add cartItem_Add)
        {
            var user = _context.Users.SingleOrDefault(s => s.Id == userId);
            if (user == null)
            {
                return false;
            }
            var product = _context.Products.Include(s => s.Shop).SingleOrDefault(p => p.Id == cartItem_Add.Id);
            if (product == null)
            {
                return false;
            }

            var userCart = _context.Carts.Include(s => s.CartItems)
                .ThenInclude(s => s.Product).SingleOrDefault(s => s.Users.Id == userId);

            if (userCart == null)
            {
                userCart = new Cart
                {
                    Users = user,
                    Shops = product.Shop,
                    CartItems = new List<CartItem>()
                };
                _context.Carts.Add(userCart);
            }
            var ExistsCartItem = userCart.CartItems.SingleOrDefault(s => s.Product.Id == cartItem_Add.Id);
            if(ExistsCartItem == null)
            {

                var cartitem = new CartItem
                {
                    Product = product,
                    option = cartItem_Add.option,
                    quantity = cartItem_Add.quantity,
                };
                userCart.CartItems.Add(cartitem);
            }
            else
            {
                ExistsCartItem.quantity++;
            }
            _context.SaveChanges();
            return true;
        }

        public bool descreaCartItem(long cartitemid)
        {
            var cartitems = _context.CartItems.SingleOrDefault(s => s.Id ==  cartitemid);
            if(cartitems == null)
            {
                return false;
            }
            else
            {
                cartitems.quantity -= 1;
                if(cartitems.quantity == 0)
                {
                    RemoveCartItem( cartitemid);
                }
                _context.SaveChanges();
                return true;
            }
        }

        public List<Cart_see> GetCartByUserId(long userId)
        {
            var userCarts = _context.Carts.Include(c => c.CartItems)
                                         .ThenInclude(s => s.Product)
                                         .ThenInclude(a => a.Shop)
                                         .Where(s => s.Users.Id == userId)
                                         .ToList();
            if(userCarts == null)
            {
                return new List<Cart_see>();
            }
            var CartList = new List<Cart_see>();
            foreach(var userCart in userCarts)
             {
                if(userCart.Shops != null)
                {
                    var CartSee = new Cart_see
                    {
                        Id = userCart.Id,
                        shop = userCart.Shops.Name,
                        list_cartitem = userCart.CartItems.Select(ci => new CartItem_see
                        {
                            Id = ci.Id,
                            Product_Id = ci.Product.Id,
                            ProductName = ci.Product.ProductName,
                            ProductPrice = ci.Product.ProductPrice,
                            Color = ci.option,
                            quantity = ci.quantity,
                        }).ToList()
                    };
                    CartList.Add(CartSee);
                }
            }
            

            return CartList;
        }

        public bool IncreaCartItem(long cartitemid)
        {
            var cartitems = _context.CartItems.SingleOrDefault(s => s.Id == cartitemid);
            if(cartitems == null) 
            {
                return false;
            }
            else
            {
                cartitems.quantity += 1;
                _context.SaveChanges();
                return true;
            }

        }

        public bool RemoveCartItem( long cartitemId)
        {
            var cartitem = _context.CartItems.SingleOrDefault(s => s.Id == cartitemId);
            if(cartitem == null)
            {
                return false ;
            }
            _context.CartItems.Remove(cartitem);
            _context.SaveChanges();
            return true;
        }
    }
}
