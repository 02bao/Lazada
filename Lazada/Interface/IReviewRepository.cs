using Lazada.Models;

namespace Lazada.Interface
{
    public interface IReviewRepository
    {
        bool CreateNew(long userid, long cartitemid, double rating, string des);
        List<Review> GetByUserId(long userId);
    }
}
