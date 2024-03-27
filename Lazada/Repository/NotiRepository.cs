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
        public List<NotiResponse> GetListNoti_buyer(long userid)
        {
            List<NotiResponse> response = new List<NotiResponse>();
            User? user = _context.Users.Where(s => s.Id ==  userid).Include(s => s.notìications).FirstOrDefault();
            if(user == null)
            {
                return response;
            }
            var buyernotification = user.notìications.Where(s => s.type_receive && s.isDelete).ToList();
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
