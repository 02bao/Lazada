using Lazada.Interface;
using Lazada.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lazada.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _addressRepository;

        public AddressController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        [HttpPost("CreateNewAddress")]
        public IActionResult CreateNewAddress(long userid, Address_User address_User)
        {
            bool tmp = _addressRepository.CreateNew(userid, address_User);
            if(tmp)
            {
                return Ok("Create Address Successfully");
            }
            return BadRequest("Error");
        }

        [HttpGet("GetAddressByUserId")]
        public IActionResult GetAddressByUserId(long userId)
        {
            var tmp = _addressRepository.GetAddressByUserId(userId);
            return Ok(tmp);
        }
    }
}
