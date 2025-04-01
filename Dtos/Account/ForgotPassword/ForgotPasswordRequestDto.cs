using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.Account.ForgotPassword
{
    public class ForgotPasswordRequestDto
    {
        [EmailAddress]
        public required string Email { get; set; }
    }
}