using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Account.ForgotPassword;

namespace MainApi.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendPasswordResetEmail(SendPasswordResetEmailDto passwordResetEmailDto);
    }
}