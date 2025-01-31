using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcologAPI.src.Domain.Entities;
using Application.DTOs;

namespace Application.Interfaces
{
    public interface IUsers
    {
        Users? Login(LoginDTO loginDTO); // v
        Users? FindOne(int id); // Pegar 1 : v
        List<Users> FindAll(int? pagina); // Pegar Todos : v
        Users Create (Users user); // Create
        void Update (Users user); //Atualizar
        void Delete (Users user); // Deletar
    }
}