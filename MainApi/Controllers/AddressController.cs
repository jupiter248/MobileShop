using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Address;
using MainApi.Application.Extensions;
using MainApi.Application.Interfaces;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Interfaces.Services;
using MainApi.Application.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Api.Controllers
{
    [ApiController]
    [Route("api/address")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllAddress()
        {
            string? username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username)) return BadRequest("Username is invalid");
            List<AddressDto> addressDtos = await _addressService.GetAllAddressAsync(username);
            return Ok(addressDtos);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAddressById([FromRoute] int id)
        {
            AddressDto addressDto = await _addressService.GetAddressByIdAsync(id);
            return Ok(addressDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] AddAddressRequestDto addAddressRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            string? username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username)) return BadRequest("Username is invalid");

            AddressDto addressDto = await _addressService.AddAddressAsync(addAddressRequestDto, username);

            return CreatedAtAction(nameof(GetAddressById), new { id = addressDto.Id }, addressDto);

        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditAddress([FromRoute] int id, [FromBody] EditAddressRequestDto editAddressRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            string? username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username)) return BadRequest("Username is invalid");
            await _addressService.EditAddressAsync(id, editAddressRequestDto, username);
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveAddress([FromRoute] int id)
        {
            string? username = User.GetUsername();
            if (string.IsNullOrWhiteSpace(username)) return BadRequest("Username is invalid");
            await _addressService.RemoveAddressAsync(id, username);
            return NoContent();

        }
    }
}