using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.Account
{
    public class NewRegisterUserDto
    {
        public string? Username { get; set; }

        public string? Email { get; set; }
    }
}