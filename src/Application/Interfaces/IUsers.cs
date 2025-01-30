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
        Users? Login(LoginDTO loginDTO);
        Users? FindOne(int id); // Pegar 1
        List<Users> FindAll(int? pagina); // Pegar Todos
        Users Create (Users user); // Create
        Users Update (Users user); //Atualizar
        Users Delete (Users user); // Deletar
    }
}