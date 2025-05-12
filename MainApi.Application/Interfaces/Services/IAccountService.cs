using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Account;
using MainApi.Application.Dtos.Account.ForgotPassword;

namespace MainApi.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<NewUserDto> RegisterUserAsync(RegisterDto registerDto);
        Task<NewUserDto> RegisterAdminAsync(RegisterDto registerDto);
        Task<NewUserDto> LoginAsync(LoginDto loginDto);
        Task<bool> ForgotPasswordAsync(ForgotPasswordRequestDto forgotPasswordRequestDto);
        Task<bool> ResetPasswordAsync(ResetPasswordRequestDto resetPasswordRequestDto);

    }
}