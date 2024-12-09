using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Account;
using MainApi.Interfaces;
using MainApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MainApi.Repository
{
    public class AccountRepository : IAccountRepository
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        public AccountRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userDtos = users.Select(u =>
                new UserDto()
                {
                    UserName = u.UserName,
                    Email = u.Email,
                    CreatedAt = u.CreatedAt
                }
            ).ToList();
            return userDtos;
        }

        public Task<NewUserDto> Login(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public Task<NewUserDto> RegisterAdmin(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }

        public Task<NewUserDto> RegisterUser(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }
    }
}