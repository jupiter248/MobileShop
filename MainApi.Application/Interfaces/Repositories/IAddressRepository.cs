using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Domain.Models.User;

namespace MainApi.Application.Interfaces.Repositories
{
    public interface IAddressRepository
    {
        Task<List<Address>?> GetAllAddressAsync(string username);
        Task<Address?> GetAddressByIdAsync(int addressId);
        Task<Address?> AddAddressAsync(Address address);
        Task<Address?> EditAddressAsync(int addressId, Address address, string username);
        Task<Address?> RemoveAddressAsync(int addressId, string username);
    }
}