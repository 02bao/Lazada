using Lazada.Data;
using Lazada.Interface;
using Lazada.Models;
using Microsoft.EntityFrameworkCore;

namespace Lazada.Repository
{
    public class NotiRepository : INotificationRepository
    {
        private readonly LazadaDBContext _context;

        public NotiRepository(LazadaDBContext context)
        {
            _context = context;
        }

        public bool CreateNotification(bool type_receive, long receive_id, string title, string descreption)
        {
            Notification notification = new Notification
            {
                title = title,
                type_receive = type_receive,
                descrepstion = descreption,
            };
            User user = new User();
            Shop shop = new Shop();
            if (type_receive)
            {// true la buyer , false la shop
                user = _context.Users.SingleOrDefault(s => s.Id == receive_id);
                if (user == null)
                {
                    return false;
                }
            }
            else
            {
                shop = _context.Shops.SingleOrDefault(s => s.Id == receive_id);
                if (shop == null)
                {
                    return false;
                }
            }
            notification.User = user;
            if (user.notifications == null )
            {
                List<Notification> _notification = new List<Notification>();
                _notification.Insert(0, notification);
                user.notifications = _notification;
            }
            else
            {
                user.notifications.Insert(0, notification);
            }
            _context.Notifications.Add(notification);
            _context.SaveChanges();
            return true;
        }

        public List<NotiResponse> GetListNoti_buyer(long userid)
        {
            List<NotiResponse> response = new List<NotiResponse>();
            User? user = _context.Users.Where(s => s.Id ==  userid).Include(s => s.notifications).FirstOrDefault();
            if(user == null)
            {
                return response;
            }
            var buyernotification = user.notifications.Where(s => s.User == user).ToList();
            foreach(var notifications in buyernotification)
            {
                response.Add(new NotiResponse
                {
                    Id = notifications.Id,
                    title = notifications.title,
                    time_sent = notifications.time_sent.AddHours(7),
                    time_seen = notifications.time_seen.AddHours(7),
                    descrepstion = notifications.descrepstion,
                });
            }
            response = response.OrderBy(s => s.time_sent).ToList();
            return response;
        }
    }
}
