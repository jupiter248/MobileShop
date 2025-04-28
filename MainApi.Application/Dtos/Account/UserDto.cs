using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Application.Dtos.Account
{
    public class UserDto
    {
        public string? UserName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}