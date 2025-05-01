using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Account;
using MainApi.Application.Interfaces;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Domain.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MainApi.Persistence.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        public UserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<List<UserDto>?> GetAllUsers()
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

        public async Task<AppUser?> GetUserByUsername(string username)
        {
            AppUser? appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (appUser == null) return null;
            return appUser;
        }
    }
}