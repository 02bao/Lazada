using AuthenticationPlugin;
using Lazada.Data;
using Lazada.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lazada.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly LazadaDBContext _context;

        public AccountsController(LazadaDBContext context )
        {
            _context = context;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(User user)
        {
            var useremail = _context.Users.SingleOrDefault(u => u.Email == user.Email);
            if(useremail != null)
            {
                return BadRequest("This account has been exists");
            }

            var userobj = new User()
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
            };
            _context.Users.Add(userobj);
            await _context.SaveChangesAsync();
            return Ok(userobj);
        }

        [HttpPost("Login")]
        public IActionResult Login(User_login user)
        {
            var userobj = _context.Users.Where(s => s.Name == user.Name
                                                &&   s.Password == user.Password).FirstOrDefault();
            if(userobj != null)
            {
                return Ok("login Successfully");
            }
            else
            {
                return BadRequest(" Login Error");
            }
        }


    }
}
