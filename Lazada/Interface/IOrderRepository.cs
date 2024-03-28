using Lazada.Models;

namespace Lazada.Interface
{
    public interface IOrderRepository
    {
        bool AddtoOrder(long userid, List<long> cartitemids, long voucherid);
        List<Order_Get> GetOrderbyUserId(long userId);
        bool CancleOrder(long orderId);
    }
}
