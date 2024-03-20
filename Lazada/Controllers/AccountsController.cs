using AuthenticationPlugin;
using Lazada.Data;
using Lazada.Interface;
using Lazada.Models;
using Lazada.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lazada.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly LazadaDBContext _context;
        private readonly IUserRepository _userRepository;

        public AccountsController(LazadaDBContext context, IUserRepository userRepository )
        {
            _context = context;
            _userRepository = userRepository;
        }

        [HttpPost("Register")]
        public  IActionResult Register(User user)
        {
            bool tmp =  _userRepository.Register(user);
            if (tmp)
            {
                return Ok("Register successfully");
            }
            else
            {
                return BadRequest("This account has been exists");
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(User_login user)
        {
            bool tmp = _userRepository.Login(user);
            if (tmp)
            {
                return Ok("Login successfully");
            }
            else
            {
                return BadRequest("Login Error");
            }
        }

        [HttpGet]
        public IActionResult GetAll( )
        {
            var user = _userRepository.GetUser();
            return Ok(user);
        }

        [HttpGet("GetbyId")]
        public IActionResult GetById(long id)
        {
            var user = _userRepository.GetById(id);
            if(user == null)
            {
                return NotFound("This account has not been exists");
            }
            return Ok(user);
        }

    }
}
