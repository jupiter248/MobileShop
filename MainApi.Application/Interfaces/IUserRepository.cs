using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Account;
using MainApi.Domain.Models.User;

namespace MainApi.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserDto>?> GetAllUsers();
        Task<AppUser?> GetUserByUsername(string username);
    }
}