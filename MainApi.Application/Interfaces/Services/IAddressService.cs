using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Address;

namespace MainApi.Application.Interfaces.Services
{
    public interface IAddressService
    {
        Task<List<AddressDto>> GetAllAddressAsync(string username);
        Task<AddressDto> GetAddressByIdAsync(int addressId);
        Task<AddressDto> AddAddressAsync(AddAddressRequestDto addAddressRequestDto, string username);
        Task EditAddressAsync(int addressId, EditAddressRequestDto editAddressRequestDto, string username);
        Task RemoveAddressAsync(int addressId, string username);
    }
}