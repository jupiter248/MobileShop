using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Address;
using MainApi.Domain.Models.User;

namespace MainApi.Application.Mappers
{
    public static class AddressMappers
    {
        public static AddressDto ToAddressDto(this Address address, string username)
        {
            return new AddressDto()
            {
                Id = address.Id,
                Country = address.Country,
                City = address.City,
                State = address.State,
                Street = address.City,
                Plate = address.Plate,
                PostalCode = address.PostalCode,
                CreatedDate = address.CreatedDate,
                Username = username
            };
        }
        public static Address ToAddressFromAdd(this AddAddressRequestDto addAddressRequestDto, AppUser appUser)
        {
            return new Address()
            {
                Country = addAddressRequestDto.Country,
                City = addAddressRequestDto.City,
                State = addAddressRequestDto.State,
                Street = addAddressRequestDto.City,
                Plate = addAddressRequestDto.Plate,
                PostalCode = addAddressRequestDto.PostalCode,
                appUser = appUser
            };
        }
        public static Address ToAddressFromEdit(this EditAddressRequestDto editAddressRequestDto)
        {
            return new Address()
            {
                Country = editAddressRequestDto.Country,
                City = editAddressRequestDto.City,
                State = editAddressRequestDto.State,
                Street = editAddressRequestDto.City,
                Plate = editAddressRequestDto.Plate,
                PostalCode = editAddressRequestDto.PostalCode,
            };
        }
    }
}