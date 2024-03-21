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

    }
}
