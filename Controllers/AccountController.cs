using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MainApi.Dtos.Account;
using MainApi.Dtos.Account.ForgotPassword;
using MainApi.Interfaces;
using MainApi.Models;
using MainApi.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MainApi.Controllers
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
        public AccountController(UserManager<AppUser> userManager, IEmailService emailService, ITokenService tokenService, SignInManager<AppUser> signInManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userRepository = userRepository;
            _emailService = emailService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(registerDto);

                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password ?? String.Empty);
                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                Username = appUser.UserName,
                                Email = appUser.Email,
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDto registerDto)
        {
            try
            {
                // if (!User.IsInRole("Admin"))
                //     return Forbid("Only admins can register new admins");

                if (!ModelState.IsValid)
                    return BadRequest(registerDto);

                var appUser = new AppUser()
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password ?? String.Empty);
                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "Admin");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto()
                            {
                                Email = appUser.Email,
                                Username = appUser.UserName,
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (System.Exception e)
            {
                return StatusCode(500, e);
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(loginDto);
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == (loginDto.Username ?? String.Empty).ToLower());

            if (user == null)
                return Unauthorized("Invalid username!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password ?? String.Empty, false);

            if (result.Succeeded == false)
                return Unauthorized("Password does not match with the user");

            var role = await _userManager.GetRolesAsync(user);

            return Ok(
                new NewUserDto
                {
                    Username = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user, role)
                }
            );
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto forgotPasswordRequest)
        {
            AppUser? appUser = await _userManager.FindByEmailAsync(forgotPasswordRequest.Email);
            if (appUser == null) return BadRequest("Email is invalid");

            var token = await _userManager.GeneratePasswordResetTokenAsync(appUser);
            string resetLink = $"https://yourwebsite.com/reset-password?email={forgotPasswordRequest.Email}&token={WebUtility.UrlEncode(token)}";



            bool emailSent = await _emailService.SendPasswordResetEmail(new SendPasswordResetEmailDto()
            {
                ResetLink = resetLink,
                ToEmail = forgotPasswordRequest.Email
            });

            if (!emailSent)
            {
                return StatusCode(500, "Failed to send email.");
            }

            return Ok("Password reset link sent to your email.");
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto resetPasswordRequest)
        {
            AppUser? user = await _userManager.FindByEmailAsync(resetPasswordRequest.Email);
            if (user == null) return BadRequest("Invalid email.");

            if (resetPasswordRequest.NewPassword != resetPasswordRequest.RepeatPassword) return BadRequest("The passwords are different");

            // Reset the password using the token
            var result = await _userManager.ResetPasswordAsync(user, WebUtility.UrlDecode(resetPasswordRequest.Token), resetPasswordRequest.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Password has been reset successfully.");
        }
    }
}