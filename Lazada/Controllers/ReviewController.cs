using Lazada.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Lazada.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewController( IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpPost("CreateNew")]
        public IActionResult CreateNew(long userid , long cartitemid, double rating, string des)
        {
            bool tmp = _reviewRepository.CreateNew(userid, cartitemid, rating, des);
            if(tmp)
            {
                return Ok("Create Successfully");
            }
            return BadRequest("Error");
        }
    }
}
