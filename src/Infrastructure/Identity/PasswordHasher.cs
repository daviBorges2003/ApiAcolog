using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AcologAPI.src.Application.Interfaces;

namespace AcologAPI.src.Infrastructure.Identity
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 128 / 8;
        private const int KeySize = 256 / 8;
        private const int Iterations = 10000;
        private static readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA256;
        private static char[] _delimeter = { ';' };

        public string hashPassword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlgorithm, KeySize);

            return string.Join(_delimeter[0], Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public bool verifyPassword(string password, string passwordHash)
        {
            var elements  = passwordHash.Split(";");
            var salt = Convert.FromBase64String(elements[0]);
            var hash = Convert.FromBase64String(elements[1]);

            var hashInput = Rfc2898DeriveBytes.Pbkdf2(password , salt, Iterations, _hashAlgorithm, KeySize);

            return CryptographicOperations.FixedTimeEquals(hash, hashInput);
        }
    }
}