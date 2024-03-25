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
        public IActionResult AddtoOrder(List<long> cartitemid, long addressid, List<long> voucherid)
        {
            bool tmp = _orderRepository.AddtoOrder(cartitemid, addressid, voucherid);
            if(tmp)
            {
                return Ok("Add Order Successfully ");
            }
            return BadRequest("Error");
        }
    }
}
