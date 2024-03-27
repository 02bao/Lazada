using Lazada.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Lazada.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepository _chatRepository;

        public ChatController(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        [HttpGet("GetConversationById")]
        public IActionResult GetConversationById(long userid, long shopid)
        {
            var tmp = _chatRepository.GetConversation_ById(userid, shopid);
            return Ok(tmp);
        }

        [HttpPost("NewMessage")]
        public IActionResult NewMessage( long userid, long shopid, string message)
        {
            bool tmp = _chatRepository.NewMessage(userid, shopid, message);
            if(tmp)
            {
                return Ok(tmp);
            }
            return BadRequest("Error");
        }
    }
}
