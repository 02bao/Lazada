using Lazada.Data;
using Lazada.Interface;
using Lazada.Models;
using Microsoft.EntityFrameworkCore;

namespace Lazada.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly LazadaDBContext _context;

        public ReviewRepository(LazadaDBContext context)
        {
            _context = context;
        }
        public bool CreateNew(long userid, long cartitemid, double rating, string des)
        {
            User user = _context.Users.SingleOrDefault(s => s.Id == userid);
            if(user == null)
            {
                return false;
            }
            CartItem cartitem = _context.CartItems.Include(s => s.order).Where( s=> s.Id == cartitemid 
                                && s.Status == Status_cart_item.order).Include( s=> s.Product).FirstOrDefault();
            if(cartitem == null)
            {
                return false;
            }
            Review review = _context.Reviews.Where(s => s.User == user &&
                    s.CartItem == cartitem).FirstOrDefault();
            if(review != null)
            {
                return false;
            }
            Review item = new Review
            {
                User = user,
                CartItem = cartitem,
                Product = cartitem.Product,
                rating = rating,
                descreption = des,
                option = cartitem.option,
            };
            _context.Reviews.Add(item);
            _context.SaveChanges();
            return true;
        }
    }
}
