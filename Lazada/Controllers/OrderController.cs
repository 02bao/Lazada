using Lazada.Interface;
using Lazada.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lazada.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpPost("AddToOrder")]
        public IActionResult AddToOrder( long cartitemId, long userId)
        {
            bool tmp = _orderRepository.AddtoOrder( cartitemId, userId);
            if(tmp)
            {
                return Ok("Add Successfully");
            }
            return BadRequest("Error");
        }
    }
}
