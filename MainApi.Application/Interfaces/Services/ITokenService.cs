using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Domain.Models;
using MainApi.Domain.Models.User;

namespace MainApi.Application.Interfaces.Services
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser, IList<string> roles);
    }
}