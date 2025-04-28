using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Application.Dtos.Account.ForgotPassword
{
    public class ResetPasswordRequestDto
    {
        [EmailAddress]
        public required string Email { get; set; }
        public required string Token { get; set; }
        public required string NewPassword { get; set; }
        public required string RepeatPassword { get; set; }

    }
}