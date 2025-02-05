using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcologAPI.src.Domain.Entities;
using Infrastructure.Persistence;
using Application.DTOs;
using Application.Interfaces;
using AcologAPI.src.Presentation.Controllers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AcologAPI.src.Application.Interfaces;

namespace Application.Services
{
    public class UserServices : IUsers
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserServices(IUserRepository repos, IMapper mapper)
        {
            _repository = repos;
            _mapper = mapper;
        }

        public async Task<UserDTO> Create(UserDTO userDTO)
        {
            var user = _mapper.Map<Users>(userDTO);
            var updatedUser = await _repository.Create(user);

            return _mapper.Map<UserDTO>(updatedUser);
        }

        public async Task<UserDTO> Update(UserDTO userDTO)
        {
            var user = _mapper.Map<Users>(userDTO);
            var updatedUser = await _repository.Update(user);

            return _mapper.Map<UserDTO>(updatedUser);
        }   

        public async Task<UserDTO> Delete(int id)
        {
            var user = await _repository.FindOne(id);
            var deleteUser = await _repository.Delete(user);

            return _mapper.Map<UserDTO>(deleteUser);
        }

        public async Task<IEnumerable<UserDTO>> FindAll(int pagina)
        {
            var users = await _repository.FindAll(pagina);
            
            return  _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> FindOne(int id)
        {
            var user = await _repository.FindOne(id);

            return _mapper.Map<UserDTO>(user);
        }

        public Users Login(LoginDTO loginDTO)
        {
            throw new NotImplementedException();
        }


    }
}