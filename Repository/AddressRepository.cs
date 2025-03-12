using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Data;
using MainApi.Interfaces;
using MainApi.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MainApi.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public AddressRepository(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public Task<Address?> AddAddressAsync(Address address, string username)
        {
            throw new NotImplementedException();
        }

        public Task<Address?> EditAddressAsync(int addressId, Address address, string username)
        {
            throw new NotImplementedException();
        }

        public Task<Address?> GetAddressByIdAsync(int addressId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Address>?> GetAllAddressAsync(string username)
        {
            List<Address>? addresses = await _context.Addresses.Include(a => a.appUser).Where(u => u.appUser.UserName == username).ToListAsync();
            if (addresses == null) return null;
            return addresses;
        }

        public Task<Address?> RemoveAddressAsync(int addressId, string username)
        {
            throw new NotImplementedException();
        }
    }
}