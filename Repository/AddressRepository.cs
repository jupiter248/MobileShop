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
        public async Task<Address?> AddAddressAsync(Address address)
        {
            await _context.AddAsync(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public async Task<Address?> EditAddressAsync(int addressId, Address address, string username)
        {
            Address? currentAddress = await _context.Addresses.Include(a => a.appUser).FirstOrDefaultAsync(a => a.Id == addressId);
            if (currentAddress != null && currentAddress.appUser.UserName == username)
            {
                currentAddress.Country = address.Country;
                currentAddress.City = address.City;
                currentAddress.State = address.State;
                currentAddress.Street = address.Street;
                currentAddress.Plate = address.Plate;
                currentAddress.PostalCode = address.PostalCode;
                await _context.SaveChangesAsync();
                return currentAddress;
            }
            else
            {
                return null;
            }
        }

        public async Task<Address?> GetAddressByIdAsync(int addressId)
        {
            Address? address = await _context.Addresses.Include(u => u.appUser).FirstOrDefaultAsync(a => a.Id == addressId);
            if (address == null) return null;
            return address;
        }

        public async Task<List<Address>?> GetAllAddressAsync(string username)
        {
            List<Address>? addresses = await _context.Addresses.Include(a => a.appUser).Where(u => u.appUser.UserName == username).ToListAsync();
            if (addresses == null) return null;
            return addresses;
        }

        public async Task<Address?> RemoveAddressAsync(int addressId, string username)
        {
            Address? address = await _context.Addresses.Include(u => u.appUser).FirstOrDefaultAsync(a => a.Id == addressId);
            if (address != null && address.appUser.UserName == username)
            {
                _context.Remove(address);
                await _context.SaveChangesAsync();
                return address;
            }
            return null;
        }
    }
}