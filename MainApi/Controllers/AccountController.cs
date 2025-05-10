using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using MainApi.Application.Dtos.Account;
using MainApi.Application.Dtos.Account.ForgotPassword;
using MainApi.Application.Interfaces;
using MainApi.Domain.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MainApi.Application.Interfaces.Services;
using MainApi.Application.Interfaces.Repositories;

namespace MainApi.Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService, UserManager<AppUser> userManager, IEmailService emailService, ITokenService tokenService, SignInManager<AppUser> signInManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userRepository = userRepository;
            _emailService = emailService;
            _accountService = accountService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(registerDto);

            NewUserDto newUser = await _accountService.RegisterUserAsync(registerDto);
            return Ok(newUser);

        }
        [Authorize(Roles = "Admin")]
        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDto registerDto)
        {
            // if (!User.IsInRole("Admin"))
            //     return Forbid("Only admins can register new admins");

            if (!ModelState.IsValid)
                return BadRequest(registerDto);

            NewUserDto newUser = await _accountService.RegisterAdminAsync(registerDto);
            return Ok(newUser);

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(loginDto);

            NewUserDto newUser = await _accountService.LoginAsync(loginDto);
            return Ok(newUser);
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto forgotPasswordRequest)
        {
            await _accountService.ForgotPasswordAsync(forgotPasswordRequest);
            return Ok("Password reset link sent to your email.");
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto resetPasswordRequest)
        {
            await _accountService.ResetPasswordAsync(resetPasswordRequest);
            return Ok("Password has been reset successfully.");
        }
    }
}