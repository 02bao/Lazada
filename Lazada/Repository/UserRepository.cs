﻿using Lazada.Data;
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

        public bool Delete(User user)
        {
            throw new NotImplementedException();
        }

        public User GetById(long id)
        {
            return _context.Users.Where( u => u.Id == id).FirstOrDefault();
        }

        public ICollection<User> GetUser()
        {
            List<User> user = _context.Users.ToList();
            return user;
        }

        public bool Login(User_login user)
        {
           var userobj = _context.Users.Where(s => s.Name == user.Name
                                                && s.Password == user.Password).FirstOrDefault();
           return userobj != null;
        }

        public bool Register(User_register userRegister)
        {
            var useremail = _context.Users.SingleOrDefault(u => u.Email == userRegister.Email);
            if (useremail != null)
            {
                return false;
            }

            var user = new User()
            {
                Name = userRegister.Name,
                Email = userRegister.Email,
                Password = userRegister.Password,
                Address = "",
                Phone = ""
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        public bool Update(User_update userupdate)
        {
            User? user = _context.Users.Where(s => s.Id == userupdate.Id).FirstOrDefault();
            if(user == null)
            {
                return false;
            }
            user.Password = userupdate.Password;
            user.Address = userupdate.Address;
            user.Phone = userupdate.Phone;
            _context.SaveChanges();
            return true;

        }
    }
}
