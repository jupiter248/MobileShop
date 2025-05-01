using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Account.ForgotPassword;

namespace MainApi.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task<bool> SendPasswordResetEmail(SendPasswordResetEmailDto passwordResetEmailDto);
    }
}