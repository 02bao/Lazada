using Lazada.Data;
using Lazada.Interface;
using Lazada.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lazada.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly LazadaDBContext _context;

        public UserRepository(LazadaDBContext context)
        { 
            _context = context;
        }

        public ICollection<User> GetUer()
        {
            throw new NotImplementedException();
        }

        public bool Login(User_login user)
        {
           var userobj = _context.Users.Where(s => s.Name == user.Name
                                                && s.Password == user.Password).FirstOrDefault();
           return userobj != null;
        }

        public bool Register(User user)
        {
            var useremail = _context.Users.SingleOrDefault(u => u.Email == user.Email);
            if (useremail != null)
            {
                return false;
            }

            var userobj = new User()
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
            };
            _context.Users.Add(userobj);
            _context.SaveChanges();
            return true;
        }
    }
}
