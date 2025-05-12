using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Address;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Interfaces.Services;
using MainApi.Application.Mappers;
using MainApi.Domain.Models.User;
using Microsoft.AspNetCore.Identity;

namespace MainApi.Infrastructure.Services.Internal
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepo;
        private readonly UserManager<AppUser> _userManager;
        public AddressService(IAddressRepository addressRepo, UserManager<AppUser> userManager)
        {
            _addressRepo = addressRepo;
            _userManager = userManager;
        }

        public async Task<AddressDto> AddAddressAsync(AddAddressRequestDto addAddressRequestDto, string username)
        {
            AppUser? appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            Address addressModel = addAddressRequestDto.ToAddressFromAdd(appUser);
            Address address = await _addressRepo.AddAddressAsync(addressModel);
            return address.ToAddressDto(username);
        }

        public async Task EditAddressAsync(int addressId, EditAddressRequestDto editAddressRequestDto, string username)
        {
            Address? addressModel = await _addressRepo.GetAddressByIdAsync(addressId);
            if (addressModel?.appUser?.UserName != username)
            {
                throw new KeyNotFoundException("username and address are not match");
            }
            Address? address = await _addressRepo.EditAddressAsync(addressId, editAddressRequestDto.ToAddressFromEdit());
            if (address == null)
            {
                throw new KeyNotFoundException("Address not found");
            }
        }

        public async Task<AddressDto> GetAddressByIdAsync(int addressId)
        {
            Address? address = await _addressRepo.GetAddressByIdAsync(addressId);
            if (address == null)
            {
                throw new KeyNotFoundException("Address not found");
            }
            if (address.appUser?.UserName == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            return address.ToAddressDto(address.appUser.UserName);
        }

        public async Task<List<AddressDto>> GetAllAddressAsync(string username)
        {
            AppUser? appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            List<Address> addresses = await _addressRepo.GetAllAddressAsync(username);
            List<AddressDto> addressDtos = addresses.Select(a => a.ToAddressDto(username)).ToList();
            return addressDtos;
        }

        public async Task RemoveAddressAsync(int addressId, string username)
        {
            Address? addressModel = await _addressRepo.GetAddressByIdAsync(addressId);
            if (addressModel?.appUser?.UserName != username)
            {
                throw new KeyNotFoundException("username and address are not match");
            }
            Address? address = await _addressRepo.RemoveAddressAsync(addressId, username);
            if (address == null)
            {
                throw new KeyNotFoundException("Address not found");
            }
        }
    }
}