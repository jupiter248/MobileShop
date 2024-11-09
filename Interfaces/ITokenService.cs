using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Models;

namespace MainApi.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}