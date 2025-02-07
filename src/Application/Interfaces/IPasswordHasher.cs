using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcologAPI.src.Application.Interfaces
{
    public interface IPasswordHasher
    {
        string hashPassword(string password);
        bool verifyPassword(string password, string passwordHash);
    }
}