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
        [HttpGet("GetById")]
        public IActionResult GetById(long id)
        {
            var tmp = _shopRepository.GetShopid(id);
            if(tmp != null)
            {
                return Ok(tmp);
            }
            return NotFound("This account has not been exists");
        }

        [HttpPost("update")]
        public IActionResult UpdateShop(Shop_update shop)
        {
            bool tmp = _shopRepository.UpdateShop(shop);
            if(tmp)
            {
                return Ok("Update successfully");
            }
            return BadRequest("Update Error");
        }

        [HttpDelete("Delete")]
        public IActionResult DeleteShop(long id)
        {
            bool tmp = _shopRepository.DeleteShop(id);
            if (tmp)
            {
                return Ok("Delete successfully");
            }
            return BadRequest("Delete Error");
        }
    }
}
