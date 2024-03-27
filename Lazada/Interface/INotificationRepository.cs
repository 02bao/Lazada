using Lazada.Models;

namespace Lazada.Interface
{
    public interface INotificationRepository
    {
        List<NotiResponse> GetListNoti_buyer(long userid);
        bool CreateNotification(bool type_receive, long receive_id, string title, string descreption);
    }
}
