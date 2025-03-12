using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Account;
using MainApi.Models.User;

namespace MainApi.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserDto>?> GetAllUsers();
        Task<AppUser?> GetUserByUsername(string username);
    }
}