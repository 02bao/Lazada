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
        public  IActionResult Register(User_register user)
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
            var Id = _userRepository.Login(user);
            if (Id < 0)
            {
                return BadRequest("Login Error");
            }
            return Ok(Id);
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

        [HttpPost("update")]
        public IActionResult Update(User_update user)
        {
            bool tmp = _userRepository.Update(user);
            if (tmp)
            {
                return Ok("update successfully");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("Delete")]
        public IActionResult Delete(long id)
        {
            bool tmp = _userRepository.Delete(id);
            if (tmp)
            {
                return Ok("Delete successfully");
            }
            else
            {
                return BadRequest("Delete Error");
            }
        }

        [HttpPut("ForgetPassword")]
        public async Task<IActionResult> ForggetPaassword(User_Forget password)
        {
            string getpassword = await _userRepository.ForgetPassword(password);
            if(getpassword != null)
            {
                return Ok(getpassword);
            }
            else
            {
                return BadRequest("Error");
            }
        }
    }
}
