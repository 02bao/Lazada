using Lazada.Models;

namespace Lazada.Interface
{
    public interface INotificationRepository
    {
        List<NotiResponse> GetListNoti_buyer(long userid);
    }
}
