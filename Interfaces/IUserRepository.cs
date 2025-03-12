using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Account;

namespace MainApi.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetAllUsers();
        Task<UserDto> GetUserByUsername(string username);
    }
}