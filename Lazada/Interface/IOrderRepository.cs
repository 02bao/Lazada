using Lazada.Models;

namespace Lazada.Interface
{
    public interface IOrderRepository
    {
        bool AddtoOrder(long userId, long cartitemId);
    }
}
