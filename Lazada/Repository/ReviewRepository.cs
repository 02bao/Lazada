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

        public List<Review> GetByUserId(long userId)
        {
            List<Review> response = new List<Review>();
            User user = _context.Users.SingleOrDefault( s=> s.Id == userId);
            if(user == null)
            {
                return response;
            }
            var review = _context.Reviews.Where(s => s.User.Id == userId)
                .Select(s => new Review
                {
                    Id = s.Id,
                    User = user,
                    CartItem = s.CartItem,
                    Product = s.Product,
                    rating = s.rating,
                    descreption = s.descreption,
                    option = s.option,
                }).ToList();
            return review;
        }
    }
}
