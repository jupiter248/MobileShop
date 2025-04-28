using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Application.Dtos.Account.ForgotPassword
{
    public class SendPasswordResetEmailDto
    {
        public required string ToEmail { get; set; }
        public required string ResetLink { get; set; }
        

    }
}