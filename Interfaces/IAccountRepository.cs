using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Account;

namespace MainApi.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<UserDto>> GetAllUsers();
        Task<NewUserDto> RegisterUser(RegisterDto registerDto);
        Task<NewUserDto> RegisterAdmin(RegisterDto registerDto);
        Task<NewUserDto> Login(LoginDto loginDto);
    }
}