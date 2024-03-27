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

        [HttpPost("CreateNotification")]
        public IActionResult CreateNotification(bool type_receive, long receive_id, string title, string descrepstion)
        {
            bool tmp = _notificationRepository.CreateNotification(type_receive, receive_id, title, descrepstion);
            if (tmp)
            {
                return Ok(tmp);
            }
            return BadRequest("Error");
        }
    }
}
