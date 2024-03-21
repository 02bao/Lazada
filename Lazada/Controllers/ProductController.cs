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

        [HttpGet("GetAll")]
        public IActionResult GetList()
        {
            var products = _productRepository.GetList();
            return Ok(products);
        }

        [HttpGet("GetbyId")]
        public IActionResult GetProductById(long id)
        {
            var products = _productRepository.GetById(id);
            if(products != null)
            {
                return Ok(products);
            }
            return BadRequest("Error");
        }

        [HttpPost("updateproduct")]
        public IActionResult UpdateProduct(Product_update productUpdate)
        {
            bool tmp = _productRepository.UpdateProduct(productUpdate);
            if(tmp)
            {
                return Ok("Update successfully");
            }
            return BadRequest("Update Error");
        }

        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct(long id)
        {
            bool tmp = _productRepository.DeleteProduct(id);
            if(tmp)
            {
                return Ok("Delete Successfully");
            }
            return BadRequest("Delete Error");
        }

        [HttpGet("GetProductByCartegoryid")]
        public IActionResult GetProductByCategoryId(long categoryid) 
        {
            var product = _productRepository.GetProductByCategoryId(categoryid);
            return Ok(product);
        }

    }
}
