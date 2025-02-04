using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class LoginDTO
    {
        public string? Email { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}