using Lazada.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Lazada.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationController(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        [HttpGet("GetListNoti_buyer")]
        public IActionResult GetListNoti_buyer(long userid)
        {
            var tmp = _notificationRepository.GetListNoti_buyer(userid);
            return Ok(tmp);
        }
    }
}
