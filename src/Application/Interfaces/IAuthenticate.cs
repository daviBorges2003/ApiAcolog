using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;

namespace AcologAPI.src.Application.Interfaces
{
    public interface IAuthenticate
    {
        Task<bool> AuthenticateAsync(string email , string senha);
        Task<bool> UserExists(string email);
        public string GeneretedToken(int id, string email);
    }
}