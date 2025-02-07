using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AcologAPI.src.Application.Interfaces;
using Application.DTOs;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AcologAPI.src.Infrastructure.Identity
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly MyDbContext _context;

        public AuthenticateService(MyDbContext context )
        {
            _context = context;
        }
        
        public async Task<bool> AuthenticateAsync(string email , string senha)
        {
            var user = await _context.Users.Where(u => u.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
            if(user == null)
            {
                return false;
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));

            for (int x = 0; x < computedHash.Length; x++)
            {
                if(computedHash[x] != user.PasswordHash[x]) return false;
            }

            return true;
        }   

        public string GeneretedToken(int id, string email)
        {
            var claim = new[]
            {
                new Claim("id", id.ToString()),
                new Claim("email", email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(DotNetEnv.Env.GetString("SECRET_KEY"))); 

            var credentials = new SigningCredentials(privateKey , SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddDays(3);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: DotNetEnv.Env.GetString("ISSUER"),
                audience: DotNetEnv.Env.GetString("AUDIENCE"),
                claims: claim,  
                expires: expiration,
                signingCredentials: credentials 
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> UserExists(string email)
        {
            var user = await _context.Users.Where(u => u.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
            if(user == null)
            {
                return false;
            }

            return true;
        }
    }
}