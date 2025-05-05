using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Address;
using MainApi.Application.Extensions;
using MainApi.Application.Interfaces;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Api.Controllers
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
            if (string.IsNullOrWhiteSpace(username)) return BadRequest("Username is invalid");
            var addresses = await _addressRepo.GetAllAddressAsync(username);
            if (addresses == null) return NotFound();
            List<AddressDto>? addressDtos = addresses.Select(a => a.ToAddressDto(username)).ToList();
            return Ok(addressDtos);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAddressById([FromRoute] int id)
        {
            var address = await _addressRepo.GetAddressByIdAsync(id);
            if (address == null) return NotFound();
            if (address.appUser?.UserName == null) return NotFound();
            return Ok(address.ToAddressDto(address.appUser.UserName));
        }
        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] AddAddressRequestDto addAddressRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            string? username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username)) return BadRequest("Username is invalid");
            var appUser = await _userRepo.GetUserByUsername(username);
            if (appUser == null) return NotFound("User is not found");
            var addressModel = addAddressRequestDto.ToAddressFromAdd(appUser);
            var address = await _addressRepo.AddAddressAsync(addressModel);
            if (address != null)
            {
                return CreatedAtAction(nameof(GetAddressById), new { id = address.Id }, address.ToAddressDto(username));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditAddress([FromRoute] int id, [FromBody] EditAddressRequestDto editAddressRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            string? username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username)) return BadRequest("Username is invalid");
            var address = await _addressRepo.EditAddressAsync(id, editAddressRequestDto.ToAddressFromEdit(), username);
            if (address != null)
            {
                return NoContent();
            }
            else
            {
                return NotFound("The address or the address with this username did not found");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveAddress([FromRoute] int id)
        {
            string? username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username)) return BadRequest("Username is invalid");
            var address = await _addressRepo.RemoveAddressAsync(id, username);
            if (address != null)
            {
                return NoContent();
            }
            else
            {
                return NotFound("The address or the address with this username did not found");
            }
        }
    }
}