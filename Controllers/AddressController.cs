using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Address;
using MainApi.Extensions;
using MainApi.Interfaces;
using MainApi.Mappers;
using MainApi.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Controllers
{
    [ApiController]
    [Route("api/address")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _addressRepo;
        private readonly IUserRepository _userRepo;
        public AddressController(IAddressRepository addressRepo, IUserRepository userRepo)
        {
            _addressRepo = addressRepo;
            _userRepo = userRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAddress()
        {
            string? username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username)) return NotFound("User is not found");
            List<Address>? addresses = await _addressRepo.GetAllAddressAsync(username);
            if (addresses == null) return NotFound();
            List<AddressDto>? addressDtos = addresses.Select(a => a.ToAddressDto(username)).ToList();
            return Ok(addressDtos);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAddressById([FromRoute] int id)
        {
            Address? address = await _addressRepo.GetAddressByIdAsync(id);
            if (address == null) return NotFound();
            if (address.appUser.UserName == null) return NotFound();
            return Ok(address.ToAddressDto(address.appUser.UserName));
        }
        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] AddAddressRequestDto addAddressRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            string? username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username)) return NotFound("Username is invalid");
            AppUser? appUser = await _userRepo.GetUserByUsername(username);
            if (appUser == null) return NotFound("User is not found");
            Address? addressModel = addAddressRequestDto.ToAddressFromAdd();
            addressModel.appUser = appUser;
            Address? address = await _addressRepo.AddAddressAsync(addressModel);
            if (address != null)
            {
                return CreatedAtAction(nameof(GetAddressById), new { id = address.Id }, address.ToAddressDto(username));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}