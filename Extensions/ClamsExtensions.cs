using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MainApi.Extensions
{
    public static class ClamsExtensions
    {
        public static string? GetUsername(this ClaimsPrincipal user)
        {
            // get the authorize user  
            string reference = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";
            string? username = user.Claims.FirstOrDefault(x => x.Type == reference)?.Value;
            if (username != null)
            {
                return username;
            }
            else
            {
                return null;
            }
        }
    }
}