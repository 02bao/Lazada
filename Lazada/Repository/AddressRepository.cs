using Lazada.Data;
using Lazada.Interface;
using Lazada.Models;
using Microsoft.EntityFrameworkCore;

namespace Lazada.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly LazadaDBContext _context;

        public AddressRepository(LazadaDBContext context)
        {
            _context = context;
        }
        public bool CreateNew(long userid, Address_User address_User)
        {
            var user = _context.Users.SingleOrDefault(s => s.Id == userid);
            if(user == null)
            {
                return false;
            }
            Address address = new Address
            {
                Users = user,
                Fullname = address_User.Fullname,
                Phone = address_User.Phone,
                City = address_User.City,
                Address_Detail = address_User.Address_Detail,
            };
            Address? tmp = _context.Addresses.Include(s => s.Users)
                                    .Where(s => s.Users == user && s.Address_Default).FirstOrDefault();
            if(tmp == null)
            {
                address.Address_Default = true;
            }
            _context.Addresses.Add(address);
            _context.SaveChanges();
            return true;
        }
    }
}
