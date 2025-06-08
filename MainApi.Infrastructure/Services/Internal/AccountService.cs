using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Account;
using MainApi.Application.Dtos.Account.ForgotPassword;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Interfaces.Services;
using MainApi.Domain.Models.User;
using Microsoft.AspNetCore.Identity;

namespace MainApi.Infrastructure.Services.Internal
{
    public class AccountService : IAccountService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        public AccountService(UserManager<AppUser> userManager, IEmailService emailService, ITokenService tokenService, SignInManager<AppUser> signInManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userRepository = userRepository;
            _emailService = emailService;
        }
        public async Task<bool> ForgotPasswordAsync(ForgotPasswordRequestDto forgotPasswordRequestDto)
        {
            var appUser = await _userManager.FindByEmailAsync(forgotPasswordRequestDto.Email);
            if (appUser == null) throw new ValidationException("Email is invalid");

            var token = await _userManager.GeneratePasswordResetTokenAsync(appUser);
            string resetLink = $"https://yourwebsite.com/reset-password?email={forgotPasswordRequestDto.Email}&token={WebUtility.UrlEncode(token)}";



            bool emailSent = await _emailService.SendPasswordResetEmail(new SendPasswordResetEmailDto()
            {
                ResetLink = resetLink,
                ToEmail = forgotPasswordRequestDto.Email
            });

            if (!emailSent)
            {
                throw new SmtpException("Failed to send email.");
            }

            return true;
        }

        public async Task<NewUserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UsernameOrEmail);

            if (user == null)
                user = await _userManager.FindByEmailAsync(loginDto.UsernameOrEmail);

            if (user == null)
                throw new UnauthorizedAccessException("User not found");

            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, loginDto.RememberMe, false);

            if (result.Succeeded == false)
                throw new UnauthorizedAccessException("Password does not match with the user");

            var role = await _userManager.GetRolesAsync(user);

            return new NewUserDto
            {
                Username = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user, role)
            };

        }

        public async Task<NewUserDto> RegisterAdminAsync(RegisterDto registerDto)
        {
            var appUser = new AppUser()
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            AppUser? userExistence = await _userManager.FindByNameAsync(appUser.UserName);
            if (userExistence != null)
            {
                throw new UnauthorizedAccessException("duplicate username");
            }

            var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password ?? String.Empty);
            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(appUser, "Admin");
                if (roleResult.Succeeded)
                {
                    return
                        new NewUserDto()
                        {
                            Email = appUser.Email,
                            Username = appUser.UserName,
                        };
                }
                else
                {
                    throw new UnauthorizedAccessException("Can not create a user with this role");
                }
            }
            else
            {
                throw new ValidationException("Password is weak");
            }
        }

        public async Task<NewUserDto> RegisterUserAsync(RegisterDto registerDto)
        {
            AppUser appUser = new AppUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            AppUser? userExistence = await _userManager.FindByNameAsync(appUser.UserName);
            if (userExistence != null)
            {
                throw new UnauthorizedAccessException("duplicate username");
            }
            var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password ?? String.Empty);
            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if (roleResult.Succeeded)
                {
                    return new NewUserDto
                    {
                        Username = appUser.UserName,
                        Email = appUser.Email,
                    };
                }
                else
                {
                    throw new UnauthorizedAccessException("Can not create a user with this role");
                }
            }
            else
            {
                throw new ValidationException("Password is weak");
            }
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordRequestDto resetPasswordRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordRequestDto.Email);
            if (user == null) throw new ValidationException("Invalid email.");

            if (resetPasswordRequestDto.NewPassword != resetPasswordRequestDto.RepeatPassword) throw new ValidationException("The passwords are different");

            // Reset the password using the token
            var result = await _userManager.ResetPasswordAsync(user, WebUtility.UrlDecode(resetPasswordRequestDto.Token), resetPasswordRequestDto.NewPassword);
            if (!result.Succeeded)
            {
                var errorMessages = result.Errors.Select(e => e.Description);
                throw new ValidationException($"Reset password failed: {string.Join(", ", errorMessages)}");
            }
            return true;
        }
    }
}