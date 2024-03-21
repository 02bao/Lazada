using Lazada.Interface;
using Lazada.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lazada.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpPost("CreateCategory")]
        public IActionResult CreateCategory(Categor_Create categorycreate, long shopid)
        {
            bool tmp = _categoryRepository.CreateCategory(categorycreate, shopid);
            if(tmp)
            {
                return Ok("Create Successfully");
            }
            return BadRequest("Create Error");
        }

        [HttpGet("GetAll")]
        public IActionResult GetList()
        {
            var category = _categoryRepository.GetList();
            return Ok(category);
        }

        [HttpGet("GetByid")]
        public IActionResult GetCategoryById(long id)
        {
            var tmp = _categoryRepository.GetbyId(id);
            if(tmp != null)
            {
                return Ok(tmp);
            }
            return BadRequest("Error");
        }

        [HttpPost("UpdateCategory")]
        public IActionResult UpdateCategory(Category_update categoryUpdate)
        {
            bool tmp = _categoryRepository.UpdateCategory(categoryUpdate);
            if(tmp)
            {
                return Ok("Update successfully");
            }
            return BadRequest("Update Error");
        }

        [HttpDelete("DeleteCategory")]
        public IActionResult DeleteCategory(long id)
        {
            bool tmp = _categoryRepository.DeleteCategory(id);
            if( tmp)
            {
                return Ok("Delete Successfully");
            }
            return BadRequest("Delete Error");
        }

        [HttpPost("GetCategoryByShopid")]
        public IActionResult GetCategoryByshopId(long shopId)
        {
            var categories = _categoryRepository.GetCategoryByshopid(shopId);
            return Ok(categories);
        }


    }
}
