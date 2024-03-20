using Lazada.Data;
using Lazada.Interface;
using Lazada.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lazada.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShopController : ControllerBase
    {
        private readonly LazadaDBContext _context;
        private readonly IShopRepository _shopRepository;

        public ShopController(LazadaDBContext context, IShopRepository shopRepository)
        {
            _context = context;
            _shopRepository = shopRepository;
        }
        [HttpPost("CreateShop")]
        public IActionResult CreateShop(Shop_Create shop)
        {
            bool  shopcreate = _shopRepository.CreateShop(shop);
            if(shopcreate == null)
            {
                return BadRequest("Create Error");
            }
            return Ok("Create Successfully");
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var shop = _shopRepository.GetList();
            return Ok(shop);
        }


    }
}
