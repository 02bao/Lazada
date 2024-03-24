﻿using Lazada.Data;
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

        public List<Address_Short> GetAddressByUserId(long userId)
        {
            List<Address_Short> response = new List<Address_Short>();
            var user = _context.Users.SingleOrDefault(s => s.Id ==  userId);    
            if( user == null ) { return response; }
            List<Address> addresses = _context.Addresses.Include(s => s.Users)
                                      .Where(s => s.Users == user).ToList();
            foreach( Address address in addresses )
            {
                Address_Short item = new Address_Short
                {
                    Address_Id = address.Id,
                    Fullname = address.Fullname,
                    Phone = address.Phone,
                    Address_Default = address.Address_Default,
                    Address_Full = address.City + " " + address.Address_Detail,
                };
                response.Add(item);
            }
            response = response.OrderByDescending(s => s.Address_Default).ToList();
            return response;
        }
    }
}