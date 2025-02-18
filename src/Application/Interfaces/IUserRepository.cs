using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcologAPI.src.Domain.Entities;


namespace AcologAPI.src.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<Users> Create(Users user);
        Task<Users> Update(Users user);
        Task<Users> Delete(Users user);
        Task<Users> FindOne(int id);
        Task<IEnumerable<Users>> FindAll(int pagina);
        Task<Users> Login(string email, string password);
    }
}