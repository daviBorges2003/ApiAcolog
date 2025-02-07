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
        Task<UserDTO> Create(UserDTO userDTO);
        Task<UserDTO> Update(UserDTO userDTO);
        Task<UserDTO> Delete(int id);
        Task<UserDTO> FindOne(int id);
        Task<IEnumerable<UserDTO>> FindAll(int pagina);
        Task<UserDTO> Login(LoginDTO loginDTO);
    }
}