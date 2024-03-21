using Lazada.Interface;
using Lazada.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lazada.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct(Product_Create productCreate, long categoryid)
        {
            bool tmp = _productRepository.CreateProduct(productCreate, categoryid);
            if(tmp)
            {
                return Ok("Create Successfully");
            }
            return BadRequest("Create Error");
        }
    }
}
